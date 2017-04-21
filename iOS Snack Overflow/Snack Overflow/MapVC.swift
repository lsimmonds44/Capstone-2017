//
//  MapVC.swift
//  Snack Overflow
//
//  Created by MBPR on 4/14/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import UIKit
import MapKit

class MapVC: UIViewController,MKMapViewDelegate,CLLocationManagerDelegate,DeliveryVCDelegate {
    
    let _driverMgr = DriverManager()
    var _driver:User!
    var _route:Route!{didSet{self.displayPin(routes: _route)}}
    let mapModel = MapVCModel()
    let _deliveryVC = DeliveryVC()

    // outlets
        private var _locationManager = CLLocationManager()
        private let delVC = DeliveryVC()
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
        //        _driverMgr.getRouteByDriverID(driverID: _driver.UserId!) { (routes, userMessage) in
        //            self.displayPin(routes: routes?[0])
        //        }
        
        // Do any additional setup after loading the view.
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    func updatePin() {
        DispatchQueue.main.async {
        let allPins = self.map.annotations
        self.map.removeAnnotations(allPins)
        self.displayPin(routes: self._route)
        }
    }
    
    
    
    func displayPin(routes:Route?){ // will probably be changed to display all pins and iterate through the list of deliveries
        
        for delivery in routes?.Deliveries ?? []{
            let pinToAdd = Pin()
            let addLine1 = (delivery.Address!.AddressLine1 ?? "")
            let addCity = (delivery.Address!.City ?? "")
            let addState = (delivery.Address!.State ?? "")
            let addZip = (delivery.Address!.Zip ?? "")
            
            mapModel.convertAddressToCoord(address: addLine1 + addCity + addState + addZip) { (returnedCoord) in
                DispatchQueue.main.async {
                    pinToAdd.title = "\(delivery.Address!.AddressLine1 ?? "")"
                    pinToAdd.subtitle = "\(delivery.DeliverDate ?? Date())"
                    pinToAdd.coordinate = returnedCoord
                    if delivery.StatusId == "Delivered"
                    {
                        pinToAdd.pinColor = "green"
                    }else{
                        pinToAdd.pinColor = "blue"
                    }
                    pinToAdd.delivery = delivery
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
    
    //Holds the delivery for the pin that has been selected
    var _selectedDelivery = Delivery()
    
    func mapView(_ mapView: MKMapView, annotationView view: MKAnnotationView, calloutAccessoryControlTapped control: UIControl) {
        let pin = view.annotation as! Pin
        
        _selectedDelivery = pin.delivery
        self.performSegue(withIdentifier: "DeliveryDetailSeg", sender: nil)
    }
    
    
    /*
     // MARK: - Navigation
     
     // In a storyboard-based application, you will often want to do a little preparation before navigation
     override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
     // Get the new view controller using segue.destinationViewController.
     // Pass the selected object to the new view controller.
     }
     */
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if segue.identifier == "DeliveryDetailSeg"{
            if let deliveryVC:DeliveryVC = segue.destination as? DeliveryVC{
                deliveryVC.navigationItem.title = "Delivery Details"
                deliveryVC._delivery = _selectedDelivery
                deliveryVC.delegate = self
            }
        }else if segue.identifier == "PickupSeg"{
            
        }
    }
}
