//
//  DeliveryManager.swift
//  Snack Overflow
//
//  Created by Robert Forbes on 4/20/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation

class DeliveryManager : NSObject{
    
    
    /**
     Update the delivery status for the delivery with the specified delivery id
     
     - Author:
     Robert Forbes
     
     - Date:
    2017/04/20
     
     - returns:
     A boolean representing success or failure and a string representing any error messages
     
     - parameters:
        - DeliveryId: The id of the delivery you want to update
        - newDeliveryStatus: A string representing the new status you want to give to the delivery
     */
    func UpdateDeliveryStatus(DeliveryId:Int, newDeliveryStatus:String, verificationImageStr:String ,completion: @escaping (_ result:Bool, _ userMessage:String)->()){
        let url:URL = URL(string:getIPAsString() + "delivery/\(DeliveryId)/\(newDeliveryStatus)/\(verificationImageStr)")!

        let task = session.dataTask(with: getRequest(url: url)) { (data, response, error) in
            do{
                if let jsonData = data, let jsonObject = try JSONSerialization.jsonObject(with: jsonData, options: .allowFragments) as? Bool{
                    let result = jsonObject
                    completion(result,"Test")
                }
                
            }catch{
                print("Error: \(error)")
                completion(false,"There was a problem communicating with the database")
            }
        }
        task.resume()
    }
    
}
