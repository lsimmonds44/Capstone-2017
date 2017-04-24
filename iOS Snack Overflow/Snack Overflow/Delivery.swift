//
//  Delivery.swift
//  Snack Overflow
//
//  Created by MBPR on 4/18/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation

/// Eric Walton
/// 2017/04/23
/// Description: Delivery Object
class Delivery: NSObject {
    
    var Address:Address?
    var DeliverDate:Date?
    var DeliveryId:Int?
    var DeliveryTypeID:String?
    var OrderID:Int?
    var Packages = [Package]()
    var StatusId:String?
    var RouteId:Int?
    
}

/// Eric Walton
/// 2017/04/23
/// Description: get's a date string and converts it to Date
///
/// - Parameter dateString: String in format of "yyyy-MM-dd'T'hh:mm:ss"
/// - Returns: Date of string if it can be converted or current date if it can't
func StringToDate(dateString:String?)-> Date
{
    var result = Date()
    let dateFormatter = DateFormatter()
    dateFormatter.dateFormat = "yyyy-MM-dd'T'hh:mm:ss"
    result = dateFormatter.date(from: dateString ?? "") ?? Date()
    return result
}

/// Eric Walton
/// 2017/04/23
/// Description: Converts a date to a specified string format
///
/// - Parameter dateToFormat: The date sent
/// - Returns: A string that represents the date passed as a Month two digit day and 4 digit year
func formatDate(dateToFormat:NSDate) -> String
{
    let dateFormatter = DateFormatter()
    dateFormatter.dateFormat = "MMMM dd yyyy"
    let dateToDisplay = dateFormatter.string(from: dateToFormat as Date)
    
    return dateToDisplay
}




