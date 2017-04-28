//
//  DriverManager.swift
//  Snack Overflow
//
//  Created by MBPR on 4/18/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation

class DriverManager: NSObject {
    
    /// Eric Walton
    /// 2017/04/23
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
                completion(nil,"Error retrieving routes!")
            }
        }
        task.resume()
    }
    
    
    /// Description: Get's pickup/ pickups for signed in driver
    /// Eric Walton
    /// 2017/04/21
    /// - Parameters:
    ///   - userID: userID description
    ///   - completion: completion description
    func getPickupByDriverID(driverID:Int, completion: @escaping (_ result:[Pickup]?, _ userMessage:String)->())
    {
        let url:URL = URL(string:getIPAsString() + "pickup/\(driverID)")!
        let task = session.dataTask(with: getRequest(url: url)) { (data, response, error) in
            do{
                if let jsonData = data,
                    let jsonObject = try JSONSerialization.jsonObject(with: jsonData, options: []) as? [[String:Any]]{
                    var pickups = [Pickup]()
                    for pickupJson in jsonObject{
                        let pickup = Pickup()
                        let address = Address()
                        pickup.DriverId = pickupJson["DriverId"] as? Int
                        pickup.EmployeeId = pickupJson["EmployeeId"] as? Int
                        pickup.PickupId = pickupJson["PickupId"] as? Int
                        pickup.SupplierId = pickupJson["SupplierId"] as? Int
                        pickup.WarehouseId = pickupJson["WarehouseId"] as? Int
                        let addressJson = pickupJson["address"] as? [String:Any]
                        address.AddressLine1 = addressJson?["AddressLineOne"] as? String
                        address.AddressLine2 = addressJson?["AddressLineTwo"] as? String
                        address.City = addressJson?["City"] as? String
                        address.State = addressJson?["State"] as? String
                        address.Zip = addressJson?["Zip"] as? String
                        address.UserId = addressJson?["UserId"] as? Int
                        address.AddressID = addressJson?["UserAddressId"] as? Int
                        pickup.Address = address
                        let PickupLineListJson = pickupJson["PickupLineList"] as? [Any]
                        for pickupLineJson in PickupLineListJson ?? []{
                            let pickupLine = PickupLine()
                            guard let pickupLineJ = pickupLineJson as? [String:Any]else{
                                continue
                            }
                            pickupLine.PickupId = pickupLineJ["PickupId"] as? Int
                            pickupLine.PickupLineId = pickupLineJ["PickupLineId"] as? Int
                            pickupLine.PickupStatus = pickupLineJ["PickupStatus"] as? Bool
                            pickupLine.ProductLotId = pickupLineJ["ProductLotId"] as? Int
                            pickupLine.productName = pickupLineJ["productName"] as? String
                            pickupLine.Quantity = pickupLineJ["Quantity"] as? Int
                            
                            pickup.PickupLineList.append(pickupLine)
                        }
                        pickups.append(pickup)
                    }
                 completion(pickups,"")
                }
            }catch{
                completion(nil,"")
            }
        }
        task.resume()
    } // end of getPickupByDriverId
    
    
    
} // end of class











