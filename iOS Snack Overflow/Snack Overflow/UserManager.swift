//
//  UserManager.swift
//  Snack Overflow
//
//  Created by MBPR on 3/30/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation


/// Description: User manager class that handles user validation to the database.
class UserManager: NSObject {
    
    
    /// Description
    /// Eric Walton
    /// 2017/14/04
    /// - Parameters:
    ///   - username: username: passed from username textfield
    ///   - password: password: passed from password textfield
    ///   - completion: completion description: When database call is done completion returns user object
    func validateLogin(username:String,password:String, completion: @escaping (_ result:User?, _ userMessage:String)->())
    {
        let user = User()
        var timer = Timer()
        var vegTimer = Timer()
        var vegArray = ["🍎","🌽","🍅","🥕"]
        var vegIndex = 0
        var output = ""
        
        let url:URL = URL(string:getIPAsString() + "user/\(username)/\(password)")!
        
        let task = session.dataTask(with: getRequest(url: url)) { (data, response, error) in
            do{
                if let jsonData = data,
                    let jsonObject = try JSONSerialization.jsonObject(with: jsonData, options: []) as? [String:Any]{
                    
                    user.UserId = jsonObject["UserId"] as? Int
                    user.FirstName = jsonObject["FirstName"] as? String
                    user.LastName = jsonObject["LastName"] as? String
                    user.Phone = jsonObject["Phone"] as? String
                    user.UserName = jsonObject["UserName"] as? String
                    user.EmailAddress = jsonObject["EmailAddress"] as? String
                    user.Active = jsonObject["Active"] as? Bool
                    completion(user,"")
                    vegTimer.invalidate()
                    timer.invalidate()
                }
            }catch{
                completion(nil,"Username or Password incorrect!")
                vegTimer.invalidate()
                timer.invalidate()
            }
        }
        
         timer = Timer.scheduledTimer(withTimeInterval: 10.0, repeats: false) { (theTimer) in
            vegTimer.invalidate()
            completion(nil,"Error connecting to database. Check for data connection. If data is present and still can't connect. Try again later.")
        }
        vegTimer = Timer.scheduledTimer(withTimeInterval: 1.0, repeats: true, block: { (theVegTimer) in
            
            
            if vegIndex < vegArray.count{
                output += vegArray[vegIndex]
                vegIndex = vegIndex + 1
                completion(nil,output)
            }else{
                vegIndex = 0
                output = ""
                completion(nil,output)
            }
        })
        
        task.resume()
    }
    
    
} // end of class




















