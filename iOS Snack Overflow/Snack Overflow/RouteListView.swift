//
//  RouteListView.swift
//  Snack Overflow
//
//  Created by MBPR on 4/20/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import UIKit

/// Eric Walton
/// 2017/04/23
/// Description: Protocol for RouteListViewDelegate
protocol RouteListViewDelegate {
    func RouteSelected(route:Route)
    func PickupSelected(pickup:Pickup)
    func RouteSelectionCanceled()
}

/// Eric Walton
/// 2017/04/23
/// Description: List view that shows routes and pickups
class RouteListView: UIView,UITableViewDelegate,UITableViewDataSource {
    
    @IBOutlet var RouteListView: RouteListView!{didSet{RouteListView.layer.cornerRadius = 8}}
    @IBOutlet weak var RouteTV: UITableView!{didSet{RouteTV.layer.cornerRadius = 8}}
    @IBOutlet weak var RouteCell: UITableViewCell!
    @IBOutlet weak var btnCancel: UIButton!{didSet{btnCancel.layer.cornerRadius = 8}}
    
    // Variables
    var delegate:RouteListViewDelegate!
    private let _driverMgr = DriverManager()
    private var _routes = [Route]()
    private var _pickups = [Pickup]()
    private var _driveType:Int! // 0 is delivery and 1 is pickup
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: Preps the RouteListView to be called as a subview
    ///
    /// - Parameters:
    ///   - apiToCall: calls the apiToCall funciton
    ///   - driverId: The signed in driver's Id
    func addView(apiToCall:String, driverId:Int)
    {
        Bundle.main.loadNibNamed("RouteListView", owner: self, options: nil)
        self.addSubview(RouteListView)
        self.RouteTV.register(RouteCell.classForCoder, forCellReuseIdentifier: RouteCell.reuseIdentifier!)
        callDriverAPI(apiToCall: apiToCall, driverId: driverId)
    }
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: Calls a Route or Pickup api from Driver Manager
    ///
    /// - Parameters:
    ///   - apiToCall: The string declaring which api to call
    ///   - driverId: The signed in driver's Id
    func callDriverAPI(apiToCall:String,driverId:Int){
        if apiToCall == "Delivery" {
            _driveType = 0
            _driverMgr.getRouteByDriverID(driverID: driverId) { (routes, userMessage) in
                DispatchQueue.main.async {
                    self._routes = routes!
                    self.RouteTV.reloadData()
                }
            }
        }else{
            _driveType = 1
            _driverMgr.getPickupByDriverID(driverID: driverId, completion: { (pickups, userMessage) in
                DispatchQueue.main.async {
                    self._pickups = pickups!
                    self.RouteTV.reloadData()
                }
            })
        }
        
    }
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: The cancel button to dismiss the view
    /// without making a selection
    /// - Parameter sender:
    @IBAction func cancelBtn(_ sender: UIButton) {
        _routes.removeAll()
        delegate?.RouteSelectionCanceled()
    }
    
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: Sets the number or sections for the table view
    ///
    /// - Parameter tableView:
    /// - Returns: 1
    func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: Sets the number of rows for the table view based on
    /// the number of items in routes or pickups
    /// - Parameters:
    ///   - tableView:
    ///   - section:
    /// - Returns: Number of items in routes or pickups
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        var count = 0
        if self._driveType == 0 {
            count = _routes.count
        }else if _driveType == 1{
            count = _pickups.count
        }
        return count
    }
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: Set's what to display for each cell
    ///
    /// - Parameters:
    ///   - tableView:
    ///   - indexPath:
    /// - Returns: The set values of each cell
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = UITableViewCell(style: .subtitle, reuseIdentifier: RouteCell.reuseIdentifier)
        if self._driveType == 0 {
            
            cell.textLabel?.text = "Delivery Date: \(formatDate(dateToFormat: _routes[indexPath.row].AssignedDate! as NSDate))"
            cell.detailTextLabel?.text = "Vehicle #: \(_routes[indexPath.row].VehicleID!) | Delivery Count: \(_routes[indexPath.row].Deliveries.count)"
        }else if self._driveType == 1{
            cell.textLabel?.text = "\(_pickups[indexPath.row].Address?.AddressLine1! ?? "")"
            cell.detailTextLabel?.text = _pickups[indexPath.row].Address?.City! ?? ""
            //            cell.textLabel?.text = "Delivery Date: \(_pickups[indexPath.row].AssignedDate!)"
            //            cell.detailTextLabel?.text = "Vehicle #: \(_pickups[indexPath.row].VehicleID!)"
        }
        
        return cell
    }
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: Called when a row is selected and set's a
    /// pickup or route variable on the HomeVC using the RouteSelected
    /// or PickupSelected delegate
    /// - Parameters:
    ///   - tableView:
    ///   - indexPath:
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        if _driveType == 0 {
            delegate?.RouteSelected(route: _routes[indexPath.row]) //.GoToPin(pinAsRecord: _pinList[indexPath.row])
        }else if _driveType == 1{
            delegate?.PickupSelected(pickup: _pickups[indexPath.row])
        }
    }
    
    //    func tableView(_ tableView: UITableView, editActionsForRowAt indexPath: IndexPath) -> [UITableViewRowAction]? {
    //        let deleteButton:UITableViewRowAction = UITableViewRowAction(style: UITableViewRowActionStyle.default, title: "Delete") { (UITalbeViewRowActionStyle, indexPath) in
    //            var pin = Pin()
    //            pin = pin.ConvertCKRecordToPin(record: self._pinList[indexPath.row])
    //            self._mVM.deletePinFromIcloud(pin: pin, completion: { (deleted) in
    //                if deleted{
    //                    DispatchQueue.main.async {
    //                        self._pinList.remove(at: indexPath.row)
    //                        tableView.reloadData()
    //                        self.delegate?.removeDeletedPin(updatedPinList: self._pinList)
    //                    }
    //                }
    //            })
    //        }
    //        deleteButton.backgroundColor = UIColor.red
    //        return [deleteButton]
    //    }
    
    
    /*
     // MARK: - Navigation
     
     // In a storyboard-based application, you will often want to do a little preparation before navigation
     override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
     // Get the new view controller using segue.destinationViewController.
     // Pass the selected object to the new view controller.
     }
     */
    
}
