//
//  DeliveryVC.swift
//  Snack Overflow
//
//  Created by Robert Forbes on 4/20/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation
import UIKit


/// Eric Walton
/// 2017/04/23
/// Description: Protocal used to update the pin to green 
///when a delivery is delivered
protocol DeliveryVCDelegate {
    func updatePin()
}


class DeliveryVC: UIViewController,UITableViewDataSource,UITableViewDelegate {
    let _deliveryMgr = DeliveryManager()
    var _delivery:Delivery!
    var delegate:DeliveryVCDelegate!
    
    @IBOutlet weak var _txtAddress: UITextView!
    @IBOutlet weak var _btnMarkDelivered: UIButton!{didSet{_btnMarkDelivered.layer.cornerRadius = 8}}
    @IBOutlet weak var _packagesTable: UITableView!
    
    /**
     - Author
     Robert Forbes
     
     -Date
     2017/04/20
     */
    override func viewDidLoad() {
        super.viewDidLoad()
        let address = _delivery.Address!
        let addLine1 = address.AddressLine1 ?? ""
        let addLine2 = address.AddressLine2 ?? ""
        let addCity = address.City ?? ""
        let addState = address.State ?? ""
        let addZip = address.Zip ?? ""
        _txtAddress.text = addLine1 + ", " + addLine2 + "\n" + addCity + ", " + addState + ", " + addZip
        _btnMarkDelivered.addTarget(self, action: #selector(DeliveryVC.btnMarkDeliveredClicked), for: .touchUpInside)
        
        _packagesTable.dataSource = self
        _packagesTable.delegate = self
    }
    
    /**
     - Author
     Robert Forbes
     
     -Date
     2017/04/20
     */
    func tableView(_ tableView: UITableView, numberOfRowsInSection section:Int) -> Int{
        return _delivery.Packages.count
        
    }
    
    /**
     - Author
     Robert Forbes
     
     -Date
     2017/04/20
     */
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let package = _delivery.Packages[indexPath.row]
        let cell = UITableViewCell(style: .subtitle, reuseIdentifier: "PackageCell")
        cell.textLabel?.text = "Package #: " + String(package.PackageId!)
        cell.detailTextLabel?.numberOfLines = 0;
        cell.detailTextLabel?.lineBreakMode = NSLineBreakMode.byWordWrapping
        var details = ""
        for line in package.PackageLineList{
            details += line.ProductName! + " - " + String(line.Quantity!);
            details += "\t"
        }
        
        cell.detailTextLabel?.text = details
        return cell
    }
    
    /**
     - Author
     Robert Forbes
     
     -Date
     2017/04/20
     */
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return 70
    }
    
    /**
     - Author
     Robert Forbes
     
     -Date
     2017/04/20
     */
    func tableView(_ tableView: UITableView, estimatedHeightForRowAt indexPath: IndexPath) -> CGFloat {
        return 70
    }
    
    /**
     Runs when the button is clicked, calls the manager to run the api call to update the delivery status
     
     - Author
     Robert Forbes
     
     -Date
     2017/04/20
     */
    func btnMarkDeliveredClicked() {
        _deliveryMgr.UpdateDeliveryStatus(DeliveryId: _delivery.DeliveryId!, newDeliveryStatus: "Delivered"){ (result, userMessage) in self.showCompletionMessage(result: result, userMessage: userMessage)
            // Eric Walton
            // 2017/04/23
            self._delivery.StatusId = "Delivered"
            self.delegate.updatePin()
        }
    }
    
    
    /**
     Shows an alert showing either the error message or a success message
     
     - Author
     Robert Forbes
     
     -Date
     2017/04/20
     */
    func showCompletionMessage(result:Bool, userMessage:String){
        var message = ""
        if(result == false){
            message = userMessage
        }else if(result == true){
            message = "Delivery Status Successfully updated"
        }
        let alertController = UIAlertController(title: "Delivery Update", message: message, preferredStyle: UIAlertControllerStyle.alert)
        alertController.addAction(UIAlertAction(title: "Dismiss", style: UIAlertActionStyle.default,handler: nil))
        
        self.present(alertController, animated: true, completion: nil)
    }
}
