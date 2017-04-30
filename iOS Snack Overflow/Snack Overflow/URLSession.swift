//
//  URLSession.swift
//  Snack Overflow
//
//  Created by MBPR on 4/18/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation



/// Makes a session
/// Eric Walton
/// 2017/14/04
let session: URLSession = {
    let config = URLSessionConfiguration.default
    return URLSession(configuration: config)
}()

/// Eric Walton
/// 2017/04/23
func getIPAsString() -> String{
//    let RobbieURLString = "http://10.108.2.56:8333/api/" // ip of computer Robbie uses.
//    let RobbieHomeURLString = "http://192.168.1.5:8333/api/" // ip of Robbies home computer
    let EricURLString = "http://10.0.1.27:8333/api/" // Home ip
//    let EricSchoolIP = "http://10.132.18.15:8333/api/" // School ip
//    let EricWorkIP =   "http://192.168.3.129:8333/api/"
    
    return EricURLString
}

/// Eric Walton
/// 2017/04/23
func getRequest(url:URL) -> URLRequest{
    return URLRequest(url: url, cachePolicy: .useProtocolCachePolicy, timeoutInterval: 60)
}
