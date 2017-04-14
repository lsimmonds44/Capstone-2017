//
//  UserManager.swift
//  Snack Overflow
//
//  Created by MBPR on 3/30/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation

class UserManager {
    
    let session: URLSession = {
        let config = URLSessionConfiguration.default
        return URLSession(configuration: config)
    }()
    
    func validateLogin(username:String,password:String, completion: @escaping (_ result:User?)->())
    {
        let user = User()
        // not implemented. Need connection to server.
        // let url:URL = SnackOverflowAPI.authUser() as URL // not currently used will probably remove Api manager
        let url2:URL = URL(string: "http://10.108.2.56:8333/api/user/\(username)/\(password)")!
        print("url: \(url2)")
        let request = URLRequest(url: url2)
        
        let task = session.dataTask(with: request) { (data, response, error) in
            do{
                if let jsonData = data,
                    //                if let jsonString = NSString(data: jsonData, encoding: String.Encoding.utf8.rawValue){
                    //                    print(jsonString)
                    //
                    //                }
                    
                    let jsonObject = try JSONSerialization.jsonObject(with: jsonData, options: []) as? [String:Any]{
                    
                    user.UserId = jsonObject["UserId"] as? Int
                    user.FirstName = jsonObject["FirstName"] as? String
                    user.LastName = jsonObject["LastName"] as? String
                    user.Phone = jsonObject["Phone"] as? String
                    user.UserName = jsonObject["UserName"] as? String
                    user.EmailAddress = jsonObject["EmailAddress"] as? String
                    user.Active = jsonObject["Active"] as? Bool
                    
                }
                completion(user)
                
            }catch let error{
                print("json error: \(error)")
                completion(nil)
                
            }
            //            }else if let requestError = error{
            //                print("Error authinticating user. Error: \(requestError)")
            //            }else{
            //                print("Unexpected error with the request")
            //            }
        }
        task.resume()
    }
    
}




















