//
//  Package.swift
//  Snack Overflow
//
//  Created by Robert Forbes on 4/19/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation

/// Eric Walton
/// 2017/04/23
/// Description: Package object
class Package: NSObject {
    
    var DeliveryId:Int?
    var OrderId:Int?
    var PackageId:Int?
    var PackageLineList = [PackageLine]()
    
}
