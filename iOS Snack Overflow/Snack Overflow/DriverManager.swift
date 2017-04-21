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
    func getRouteByDriverID(driverID:Int, completion: @escaping (_ result:[Route]?, _ userMessage:String)->())
    {
        let url:URL = URL(string:getIPAsString() + "route/\(driverID)")!
        
        let task = session.dataTask(with: getRequest(url: url)) { (data, response, error) in
            do{
                if let jsonData = data,
                    let jsonObject = try JSONSerialization.jsonObject(with: jsonData, options: []) as? [[String:Any]]{
                    // print("jsonOb \(jsonObject)")
                    var driverRoutes = [Route]()
                    
                    for route in jsonObject
                    {
                        let driverRoute = Route()
                        driverRoute.DriverID =  route["DriverId"] as? Int
                        driverRoute.RouteID = route["RouteId"] as? Int
                        driverRoute.VehicleID = route["VehicleId"] as? Int
                        driverRoute.AssignedDate = StringToDate(dateString: route["AssignedDate"] as? String)
                        let routes = route["Deliveries"] as? [Any]
                        for route in routes ?? []
                        {
                            let delivery = Delivery()
                            let address = Address()
                            guard let json = route as? [String:Any] else
                            {
                                continue
                            }
                            delivery.DeliveryId = json["DeliveryId"] as? Int
                            delivery.DeliveryTypeID = json["DeliveryTypeId"] as? String
                            delivery.OrderID = json["OrderId"] as? Int
                            delivery.RouteId = json["RouteId"] as? Int
                            delivery.StatusId = json["StatusId"] as? String
                            delivery.DeliverDate =  StringToDate(dateString:json["DeliveryDate"] as? String)
                            let addressJson = json["Address"] as? [String:Any]
                            address.AddressLine1 = addressJson?["AddressLineOne"] as? String
                            address.AddressLine2 = addressJson?["AddressLineTwo"] as? String
                            address.City = addressJson?["City"] as? String
                            address.State = addressJson?["State"] as? String
                            address.Zip = addressJson?["Zip"] as? String
                            delivery.Address = address
                            let packageList = json["PackageList"] as? [Any]
                            for package in packageList ?? []
                            {
                                let packageOb = Package()
                                let packageLineOb = PackageLine()
                                guard let packageJson = package as? [String:Any] else
                                {
                                    continue
                                }
                                packageOb.DeliveryId = packageJson["DeliveryId"] as? Int
                                packageOb.OrderId = packageJson["OrderId"] as? Int
                                packageOb.PackageId = packageJson["PackageId"] as? Int
                                let packageLineList = packageJson["PackageLineList"] as? [Any]
                                for packageLine in packageLineList ?? []
                                {
                                    guard let packageLineJson = packageLine as? [String:Any]else
                                    {
                                        continue
                                    }
                                    packageLineOb.PackageId = packageLineJson["PackageId"] as? Int
                                    packageLineOb.PackageLineId = packageLineJson["PackageLineId"] as? Int
                                    packageLineOb.PricePaid = packageLineJson["PricePaid"] as? Double
                                    packageLineOb.ProductLotId = packageLineJson["ProductLotId"] as? Int
                                    packageLineOb.ProductName = packageLineJson["ProductName"] as? String
                                    packageLineOb.Quantity = packageLineJson["Quantity"] as? Int
                                    packageOb.PackageLineList.append(packageLineOb)
                                }
                                delivery.Packages.append(packageOb)
                            }
                            driverRoute.Deliveries.append(delivery)
                        }
                        driverRoutes.append(driverRoute)
                    }
                    completion(driverRoutes,"")
                }
            }catch{
                completion(nil,"Username or Password incorrect!")
            }
        }
        task.resume()
    }
    
    
    /// Description: Get's pickup/ pickups for signed in driver
    ///
    /// - Parameters:
    ///   - userID: userID description
    ///   - completion: completion description
    func getPickupByDriverID(driverID:Int, completion: @escaping (_ result:Pickup, _ userMessage:String)->())
    {
        
        
    }
    
    
    
    
} // end of class
