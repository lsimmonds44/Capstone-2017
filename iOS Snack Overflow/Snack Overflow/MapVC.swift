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
        mapModel.convertAddressToCoord(address: "6301 Kirkwood Blvd SW Cedar Rapids, IA 52404") { (returnedCoord) in
            DispatchQueue.main.async {
                self.displayPin(coord: returnedCoord)
            }
        }
        // Do any additional setup after loading the view.
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    
    
    func displayPin(coord:CLLocationCoordinate2D){ // will probably be changed to display all pins and iterate through the list of deliveries
        let pinToAdd = Pin()
            pinToAdd.title = "Kirkwood"
            pinToAdd.subtitle = "Test"
            pinToAdd.coordinate = coord
            pinToAdd.pinColor = "blue"
            self.map.addAnnotation(pinToAdd)
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
    
    /*
     // MARK: - Navigation
     
     // In a storyboard-based application, you will often want to do a little preparation before navigation
     override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
     // Get the new view controller using segue.destinationViewController.
     // Pass the selected object to the new view controller.
     }
     */
    
}
