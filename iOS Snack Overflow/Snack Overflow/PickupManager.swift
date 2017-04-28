//
//  PickupManager.swift
//  Snack Overflow
//
//  Created by Robert Forbes on 4/23/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation

class PickupManager{
    
    
    /**
     Updates the status of each pickup line for the passed in pickup
     
     - Author:
     Robert Forbes
     
     - Date:
     2017/04/23
     
     - returns:
     A boolean representing success or failure and a string representing any error messages
     
     - parameters:
     - pickup: The pickup you want to update
     */
    func UpdatePickupStatus(pickup:Pickup, completion: @escaping (_ result:Bool, _ userMessage:String)->()){
        
        var endResult = true
        var endMessage = ""
        
        for line in pickup.PickupLineList{
            self.UpdatePickupLineStatus(pickupLineId: line.PickupLineId!){ (result, userMessage) in
                if(!result){
                    endResult = result
                    endMessage = userMessage
                }
            }
        }
        
        completion(endResult,endMessage)
        
        
    }
    
    /**
     Update the pickup status for the pickup line with the specified pickup line id
     
     - Author:
     Robert Forbes
     
     - Date:
     2017/04/23
     
     - returns:
     A boolean representing success or failure and a string representing any error messages
     
     - parameters:
     - pickupLineId: The id of the pickup you want to update
     */
    func UpdatePickupLineStatus(pickupLineId:Int, completion: @escaping (_ result:Bool, _ userMessage:String)->()){
        
        let url:URL = URL(string:getIPAsString() + "pickup/markpickedup/\(pickupLineId)/")!
        
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
