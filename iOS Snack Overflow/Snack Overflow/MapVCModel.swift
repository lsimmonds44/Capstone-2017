//
//  MapVCModel.swift
//  Snack Overflow
//
//  Created by MBPR on 4/15/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation
import MapKit

class MapVCModel: NSObject, MKMapViewDelegate,CLLocationManagerDelegate {
    
    // outlets
    private var _locationManager = CLLocationManager()
    var geoCoder = CLGeocoder(){didSet{_locationManager.delegate = self}}
    
    
    func convertAddressToCoord(address:String,completion: @escaping (_ result:CLLocationCoordinate2D)->()){
        var coord:CLLocationCoordinate2D?
            geoCoder = CLGeocoder()
            geoCoder.geocodeAddressString(address) { (placeMark, error) in
            if nil == error{
            coord = placeMark?.first?.location?.coordinate
            print("coord: \(coord!.latitude) \(coord!.longitude)")
            completion(coord!)
            }else{
                print(error!)
            }
        }
    }
    
    /// Description
    /// Switches the pins color based on what color name is stored with the pin in iCloud
    /// Called from mapview view for annotations
    /// - Parameter pin: The current pin being dropped
    /// - Returns: The color the pin has stored or a default red.
    func switchPinColor(pin:Pin) -> UIColor{
        var color = UIColor()
        
        switch pin.pinColor {
        case "white":
            color = UIColor.white
            break
        case "blue":
            color = UIColor.blue
            break
        case "orange":
            color = UIColor.orange
            break
        case "green":
            color = UIColor.green
            break
        case "brown":
            color = UIColor.brown
            break
        case "slate":
            color = UIColor.gray
            break
        default:
            color = UIColor.red
        }
        return color
    }
    
} // end of class
