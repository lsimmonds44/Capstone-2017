//
//  PinClass.swift
//  RockPicker
//
//  Created by MBPR on 3/28/16.
//  Copyright © 2016 NewComInc. All rights reserved.
//

import Foundation
import MapKit
import CloudKit

class Pin : NSObject, MKAnnotation {
    
    var coorLat = Double()
    var coorLong = Double()
    var coordinate = CLLocationCoordinate2D()
    var title: String?
    var subtitle: String?
    var pinColor = String()
    var pinImage: CKAsset?
    var pinCreationDate = NSDate()
    var pinID: CKRecordID?
    var delivery = Delivery()
    
    override init() {
        
    }
    
    init(coordinate: CLLocationCoordinate2D, title: String, subtitle: String, pinColor: String, pinImage:CKAsset) {
        self.coordinate = coordinate
        self.title = title
        self.subtitle = subtitle
        self.pinColor = pinColor
        self.pinImage = pinImage
    }
    
    init(coordinate: CLLocationCoordinate2D, title: String, subtitle: String, pinColor: String) {
        self.coordinate = coordinate
        self.title = title
        self.subtitle = subtitle
        self.pinColor = pinColor
    }
    
    
    
    
} // end of class










