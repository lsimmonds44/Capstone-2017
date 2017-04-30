//
//  MapVC.swift
//  Snack Overflow
//
//  Created by MBPR on 4/14/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import UIKit
import MapKit

class MapVC: UIViewController,MKMapViewDelegate,CLLocationManagerDelegate,DeliveryVCDelegate,PickupVCDelegate {
    
    /// Eric Walton
    /// 2017/04/23
    // Private variables
    private let _driverMgr = DriverManager()
    private let mapModel = MapVCModel()
    private let _deliveryVC = DeliveryVC()
    private var _locationManager = CLLocationManager()
    private let delVC = DeliveryVC()
    private let kirkwoodCoord = CLLocationCoordinate2D(latitude: 41.910438, longitude: -91.651125) // AKA Snack OverFlow Base
    
    /// Eric Walton
    /// 2017/04/23
    // Global variables
    var _driver:User!
    var _route:Route!{didSet{self.displayAllRoutePins(routes: _route)}}
    var _pickup:Pickup!{didSet{self.displayPickupPin(pickup: _pickup)}}
    
    /// Eric Walton
    /// 2017/04/23
    // outlets
    @IBOutlet weak var map: MKMapView!{didSet{
        map.delegate = self
        map.mapType = .hybrid
        map.showsUserLocation = true
        //map.setUserTrackingMode(MKUserTrackingMode.follow, animated: true)
        _locationManager.delegate = self
        map.isRotateEnabled = false
        _locationManager.requestAlwaysAuthorization()
        _locationManager.desiredAccuracy = kCLLocationAccuracyBest
        _locationManager.startUpdatingLocation()
        let span = MKCoordinateSpanMake(0.05, 0.05)
        let region = MKCoordinateRegion(center: kirkwoodCoord, span: span)
        map.setRegion(region, animated: true)
        }}
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: App lifecycle handling not used at this time
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
    }
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: App lifecycle handling not used at this time
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: delegate method of DeliveryVC to update the pin
    /// to green when a delivery is marked delivered
    func updatePin() {
        DispatchQueue.main.async {
            let allPins = self.map.annotations
            self.map.removeAnnotations(allPins)
            if(self._route != nil){
                self.displayAllRoutePins(routes: self._route)
            }
            if(self._pickup != nil){
                self.displayPickupPin(pickup: self._pickup)
            }
            
        }
    }
    
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: Takes a route object and iterates through the deliveries
    /// in the route getting the address of each and converting to coordinates
    /// drops a pin at each location.
    /// - Parameter routes: Route holds infomation about the deliveries for the route i.e addresses and dates
    func displayAllRoutePins(routes:Route?){
        
        for delivery in routes?.Deliveries ?? []{
            let pinToAdd = Pin()
            let addLine1 = (delivery.Address!.AddressLine1 ?? "")
            let addCity = (delivery.Address!.City ?? "")
            let addState = (delivery.Address!.State ?? "")
            let addZip = (delivery.Address!.Zip ?? "")
            
            mapModel.convertAddressToCoord(address: addLine1 + " " + addCity + " " + addState + " " + addZip) { (returnedCoord) in
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
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: get's a pickup object takes the address
    /// and calls a method to convert to coordinates then drops a pin at
    /// that location
    /// - Parameter pickup: Pickup object holds infomation about the pickup i.e address
    func displayPickupPin(pickup:Pickup){
        
        let pinToAdd = Pin()
        let addLine1 = (pickup.Address!.AddressLine1 ?? "")
        let addCity = (pickup.Address!.City ?? "")
        let addState = (pickup.Address!.State ?? "")
        let addZip = (pickup.Address!.Zip ?? "")
        
        mapModel.convertAddressToCoord(address: addLine1 + " " + addCity + " " + addState + " " + addZip) { (returnedCoord) in
            DispatchQueue.main.async {
                pinToAdd.title = "\(pickup.Address!.AddressLine1 ?? "")"
                pinToAdd.subtitle = pickup.Address?.City
                pinToAdd.coordinate = returnedCoord
                pinToAdd.pickup = pickup
                var pickedupCount = 0
                for pickupLine in pickup.PickupLineList{
                    if pickupLine.PickupStatus!
                    {
                        pickedupCount = pickedupCount + 1
                    }
                }
                if pickedupCount == pickup.PickupLineList.count
                {
                    pinToAdd.pinColor = "green"
                }else{
                    pinToAdd.pinColor = "blue"
                }
                
                self.map.addAnnotation(pinToAdd)
            }
        }
    }
    
    
    /// Eric Walton
    /// 2017/04/23
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
    
    //Holds the delivery/pickup for the pin that has been selected
    var _selectedDelivery = Delivery()
    var _selectedPickup = Pickup()
    
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: Method that is called when the pin info callout is pressed
    /// Used to pass the selected route or pickup to thier detail view using
    /// a segue
    /// - Parameters:
    ///   - mapView: The map view
    ///   - view:
    ///   - control:
    func mapView(_ mapView: MKMapView, annotationView view: MKAnnotationView, calloutAccessoryControlTapped control: UIControl) {
        let pin = view.annotation as! Pin
        if _route != nil {
            _selectedDelivery = pin.delivery
            self.performSegue(withIdentifier: "DeliveryDetailSeg", sender: nil)
        }else{
            _selectedPickup = pin.pickup
            self.performSegue(withIdentifier: "PickupDetailSeg", sender: nil)
        }
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
        }else if segue.identifier == "PickupDetailSeg"{
            if let PickupVC:PickupVC = segue.destination as? PickupVC{
                PickupVC.navigationItem.title = "Pickup Details"
                PickupVC._pickup = _selectedPickup
                PickupVC.delegate = self
            }
        }
    }
}
