//
//  ApiManager.swift
//  Snack Overflow
//
//  Created by MBPR on 4/7/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation

enum Method:String{
    case getUser = "user/method=authenticateuser/"
}

struct   SnackOverflowAPI {
    let ipAddress = ""
    private static let baseURLString = "https://10.108.2.56:8333/api/"
    
    private static func snackOverflowURL(method: Method, parameters: [String:String]?) -> NSURL{
        let components = NSURLComponents(string: baseURLString)
        var queryItems = [NSURLQueryItem]()
        
        if let additionalParams = parameters{
            for (key,value) in additionalParams{
                let item = NSURLQueryItem(name: key, value: value)
                queryItems.append(item)
            }
        }
        components?.queryItems = queryItems as [URLQueryItem]
        
        return components!.url! as NSURL
    }
    
    static func authUser() -> NSURL{
        return snackOverflowURL(method: .getUser, parameters: ["username" : "password"])
    }
    
    
    
}
