//
//  UserManager.swift
//  Snack Overflow
//
//  Created by MBPR on 3/30/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation


/// Description: User manager class that handles user validation to the database.
class UserManager {
    
    
    
    /// Makes a session
    /// Eric Walton
    /// 2017/14/04
    let session: URLSession = {
        let config = URLSessionConfiguration.default
        return URLSession(configuration: config)
    }()
    
    
    /// Description
    /// Eric Walton
    /// 2017/14/04
    /// - Parameters:
    ///   - username: username: passed from username textfield
    ///   - password: password: passed from password textfield
    ///   - completion: completion description: When database call is done completion returns user object
    func validateLogin(username:String,password:String, completion: @escaping (_ result:User?)->())
    {
        let user = User()
        // not implemented. Need connection to server.
        let url:URL = URL(string: "http://192.168.3.129:8333/api/user/\(username)/\(password)")! // uses ip from computer Robbie usually sits.
        let url2:URL = URL(string: "http://10.108.2.56:8333/api/user/\(username)/\(password)")! // ip changes depending on where I'm working.
        let request = URLRequest(url: url)
        
        let task = session.dataTask(with: request) { (data, response, error) in
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
                    completion(user)
                }
            }catch{
                completion(nil)
            }
        }
        completion(user)
        task.resume()
    }
}




















