//
//  AppUser.swift
//  Snack Overflow
//
//  Created by MBPR on 4/14/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation

/// Eric Walton
/// 2017/04/23
/// Description User object
class User: NSObject {
    var UserId:Int?
    var FirstName:String?
    var LastName:String?
    var Phone:String?
    var PreferredAddressId:Int?
    var EmailAddress:String?
    var EmailPreferences:Bool?
    var UserName:String?
    var Active:Bool?
    
    override init(){}
    
    init(userId:Int!,firstName:String!, lastName:String!, phone:String!,preferredAddressId:Int!, emailAddress:String!,
        emailPreferences:Bool!, userName:String!,active:Bool!
        ) {
        UserId = userId
        FirstName = firstName
        LastName = lastName
        Phone = phone
        PreferredAddressId = preferredAddressId
        EmailAddress = emailAddress
        EmailPreferences = emailPreferences
        UserName = userName
        Active = active
    }
    
}
