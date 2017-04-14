//
//  ApiManager.swift
//  Snack Overflow
//
//  Created by MBPR on 4/7/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation

//enum Method:String{
////    case getUser = "user/method=authenticateuser/"
//    case getUser = "user/"
//}
//
//struct   SnackOverflowAPI {
//    let ipAddress = ""
//    private static let baseURLString = "https://10.0.1.27:8333/api/" // The ipaddress will need to change depending on the computer the app is talkin to
//    
//    private static func snackOverflowURL(method: Method, parameters: [String:String]?) -> URL{
//        let components = NSURLComponents(string: baseURLString)!
//        var queryItems = [NSURLQueryItem]()
//        
//        let baseParams = [
//            "Method": method.rawValue,
//            "format": "json",
//            "nojsoncallback": "1"
//        ]
//        
//        for (key, value) in baseParams {
//            let item = NSURLQueryItem(name: key, value: value)
//            queryItems.append(item)
//        }
//        
//        
//        if let additionalParams = parameters{
//            for (key,value) in additionalParams{
//                let item = NSURLQueryItem(name: key, value: value)
//                queryItems.append(item)
//            }
//        }
//        components.queryItems = queryItems as [URLQueryItem]
//        
//        return components.url!
//    }
//    
//    static func authUser() -> URL{
//        return snackOverflowURL(method: .getUser, parameters: ["ADMIN" : "ADMIN"])
//    }
//    
//    
//    
//}
