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

func getIPAsString() -> String{
//    let RobbieURLString = "http://10.108.2.56:8333/api/" // ip of computer Robbie uses.
//    let EricURLString = "http://10.0.1.27:8333/api/" // Home ip
//    let EricSchoolIP = "http://10.132.18.15:8333/api/" // School ip
    let EricWorkIP =   "http://192.168.3.129:8333/api/"
    
    return EricWorkIP
}

func getRequest(url:URL) -> URLRequest{
    return URLRequest(url: url, cachePolicy: .useProtocolCachePolicy, timeoutInterval: 60)
}
