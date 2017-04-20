//
//  MapVC.swift
//  Snack Overflow
//
//  Created by MBPR on 4/14/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import UIKit
import MapKit

class MapVC: UIViewController,MKMapViewDelegate,CLLocationManagerDelegate {
    
    let _driverMgr = DriverManager()
    var _driver:User!
    
    // outlets
    let mapModel = MapVCModel()
    private var _locationManager = CLLocationManager()
    @IBOutlet weak var map: MKMapView!{didSet{
        map.delegate = self
        map.mapType = .hybrid
        map.showsUserLocation = true
        map.setUserTrackingMode(MKUserTrackingMode.follow, animated: true)
        _locationManager.delegate = self
        map.isRotateEnabled = false
        _locationManager.requestAlwaysAuthorization()
        _locationManager.desiredAccuracy = kCLLocationAccuracyBest
        _locationManager.startUpdatingLocation()
        }}
    
    
    
    
    override func viewDidLoad() {
        super.viewDidLoad()
        _driverMgr.getRouteByDriverID(driverID: _driver.UserId!) { (route, userMessage) in
            self.displayPin(routes: route)
        }
        
        // Do any additional setup after loading the view.
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    
    
    func displayPin(routes:Route?){ // will probably be changed to display all pins and iterate through the list of deliveries
        
        for delivery in routes?.Deliveries ?? []{
            
                let addLine1 = (delivery.Address!.AddressLine1 ?? "")
                let addLine2 = (delivery.Address!.AddressLine2 ?? "")
                let addCity = (delivery.Address!.City ?? "")
                let addState = (delivery.Address!.State ?? "")
                let addZip = (delivery.Address!.Zip ?? "")
                
                mapModel.convertAddressToCoord(address: addLine1 + addLine2 + addCity + addState + addZip) { (returnedCoord) in
                    DispatchQueue.main.async {
                        let pinToAdd = Pin()
                        pinToAdd.title = "\(delivery.Address!.AddressLine1 ?? "")"
                        pinToAdd.subtitle = "\(delivery.OrderID ?? 0)"
                        pinToAdd.coordinate = returnedCoord
                        pinToAdd.pinColor = "blue"
                        self.map.addAnnotation(pinToAdd)
                    }
                }
        }
        
        
        
        
        
    }
    
    /// Description
    /// Get's called when an annotation is dropped
    /// The pin color is set by calling switchPinColor in the MapViewModel
    /// - Parameters:
    ///   - mapView: mapView description
    ///   - annotation: Custom pin made in PinClass
    /// - Returns: the modified pin
    func mapView(_ mapView: MKMapView, viewFor annotation: MKAnnotation) -> MKAnnotationView? {
        if annotation.isKind(of: MKUserLocation.self) {
            return nil
        }
        let inPin:Pin = annotation as! Pin
        let outPin =  MKPinAnnotationView.init(annotation: annotation, reuseIdentifier: "newPin")
        outPin.canShowCallout = true
        outPin.animatesDrop = true
        outPin.pinTintColor = mapModel.switchPinColor(pin: inPin)
        outPin.rightCalloutAccessoryView = UIButton.init(type: UIButtonType.detailDisclosure)
        
        return outPin
    }
    
    func mapView(_ mapView: MKMapView, annotationView view: MKAnnotationView, calloutAccessoryControlTapped control: UIControl) {
//        let pin = view.annotation as! Pin
        // Robbie use this to call the view you want to make for detail view.
    }
    
    
    /*
     // MARK: - Navigation
     
     // In a storyboard-based application, you will often want to do a little preparation before navigation
     override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
     // Get the new view controller using segue.destinationViewController.
     // Pass the selected object to the new view controller.
     }
     */
    
}
