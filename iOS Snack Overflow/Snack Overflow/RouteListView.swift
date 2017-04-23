//
//  RouteListView.swift
//  Snack Overflow
//
//  Created by MBPR on 4/20/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import UIKit

protocol RouteListViewDelegate {
    func RouteSelected(route:Route)
    func PickupSelected(pickup:Pickup)
    func RouteSelectionCanceled()
}

class RouteListView: UIView,UITableViewDelegate,UITableViewDataSource {
    
    @IBOutlet var RouteListView: RouteListView!{didSet{RouteListView.layer.cornerRadius = 8}}
    @IBOutlet weak var RouteTV: UITableView!{didSet{RouteTV.layer.cornerRadius = 8}}
    @IBOutlet weak var RouteCell: UITableViewCell!
    @IBOutlet weak var btnCancel: UIButton!{didSet{btnCancel.layer.cornerRadius = 8}}
    
    // Variables
    var delegate:RouteListViewDelegate!
    let _driverMgr = DriverManager()
    var _routes = [Route]()
    var _pickups = [Pickup]()
    var _driveType:Int!
    
    func addView(apiToCall:String, driverId:Int)
    {
        Bundle.main.loadNibNamed("RouteListView", owner: self, options: nil)
        self.addSubview(RouteListView)
        self.RouteTV.register(RouteCell.classForCoder, forCellReuseIdentifier: RouteCell.reuseIdentifier!)
        callDriverAPI(apiToCall: apiToCall, driverId: driverId)
    }
    
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
    
    @IBAction func cancelBtn(_ sender: UIButton) {
        _routes.removeAll()
        delegate?.RouteSelectionCanceled()
    }
    
    
    func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        var count = 0
        if self._driveType == 0 {
            count = _routes.count
        }else if _driveType == 1{
            count = _pickups.count
        }
        return count
    }
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
