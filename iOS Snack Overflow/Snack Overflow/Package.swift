//
//  Package.swift
//  Snack Overflow
//
//  Created by Robert Forbes on 4/19/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation

class Package: NSObject {
    
    var DeliveryId:Int?
    var OrderId:Int?
    var PackageId:Int?
    var PackageLineList = [PackageLine]()
    
}
