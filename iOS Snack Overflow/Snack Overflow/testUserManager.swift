//
//  testUserManager.swift
//  Snack Overflow
//
//  Created by MBPR on 3/30/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation

class TestUserManager {
    func validateLogin(username:String,password:String, completion: @escaping (_ result:Bool)->())
    {
        if username == "ADMIN" && password == "ADMIN" {
            completion(true)
        }else{
            completion(false)
        }
    }
}










