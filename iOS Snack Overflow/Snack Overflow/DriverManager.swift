//
//  DriverManager.swift
//  Snack Overflow
//
//  Created by MBPR on 4/18/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation

class DriverManager: NSObject {


    /// Description: Get's route/routes for signed in driver
    ///
    /// - Parameters:
    ///   - userID: userID description
    ///   - completion: completion description
    func getRouteByDriverID(driverID:Int, completion: @escaping (_ result:Route?, _ userMessage:String)->())
    {
         let url:URL = URL(string:getIPAsString() + "route/\(driverID)")!
        
        let task = session.dataTask(with: getRequest(url: url)) { (data, response, error) in
            do{
                
                if let jsonData = data,
                    let jsonObject = try JSONSerialization.jsonObject(with: jsonData, options: []) as? [[String:Any]]{
                   // print("jsonOb \(jsonObject)")
                    let driverRoute = Route()
                    
                    
            
                    
                    for route in jsonObject{
                        let routes = route["Deliveries"] as? [Any]
                        for route in routes ?? []{
                            let address = Address()
                            let delivery = Delivery()
                            guard let json = route as? [String:Any] else
                            {
                                continue
                            }
                            delivery.DeliveryId = json["DeliveryId"] as? Int
                            let addressJson = json["Address"] as? [String:Any]
                            address.AddressLine1 = addressJson?["AddressLineOne"] as? String
                            address.AddressLine2 = addressJson?["AddressLineTwo"] as? String
                            address.City = addressJson?["City"] as? String
                            address.State = addressJson?["State"] as? String
                            address.Zip = addressJson?["Zip"] as? String
                            delivery.Address = address
                            driverRoute.Deliveries.append(delivery)
                            
                        }
                        
                    }
                    completion(driverRoute,"")
                    
//                    for package in route.Deliveries{
//                        print(package.Address?.AddressLine1 ?? "No address")
//                    }
                    
                    
//                    user.UserId = jsonObject["UserId"] as? Int
                }
            }catch{
                completion(nil,"Username or Password incorrect!")
//                vegTimer.invalidate()
//                timer.invalidate()
            }
        }
        task.resume()
    }
    
    
    /// Description: Get's pickup/ pickups for signed in driver
    ///
    /// - Parameters:
    ///   - userID: userID description
    ///   - completion: completion description
    func getPickupByDriverID(userID:Int, completion: @escaping (_ result:Pickup, _ userMessage:String)->())
    {
        
        
    }

    
    

} // end of class
