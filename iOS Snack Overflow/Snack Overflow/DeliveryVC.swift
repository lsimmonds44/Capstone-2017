//
//  DeliveryVC.swift
//  Snack Overflow
//
//  Created by Robert Forbes on 4/20/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation
import UIKit


class DeliveryVC: UIViewController,UITableViewDataSource,UITableViewDelegate {
    let _deliveryMgr = DeliveryManager()
    var _delivery:Delivery!
    
    @IBOutlet weak var _addressLine1: UILabel!
    @IBOutlet weak var _addressLine2: UILabel!
    @IBOutlet weak var _addressCity: UILabel!
    @IBOutlet weak var _addressState: UILabel!
    @IBOutlet weak var _addressZip: UILabel!
    
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
        _addressLine1.text = _delivery.Address!.AddressLine1 ?? ""
        _addressLine2.text = _delivery.Address!.AddressLine2 ?? ""
        _addressCity.text = _delivery.Address!.City ?? ""
        _addressState.text = _delivery.Address!.State ?? ""
        _addressZip.text = _delivery.Address!.Zip ?? ""
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
        return UITableViewAutomaticDimension
    }
    
    /**
     - Author
     Robert Forbes
     
     -Date
     2017/04/20
     */
    func tableView(_ tableView: UITableView, estimatedHeightForRowAt indexPath: IndexPath) -> CGFloat {
        return 50
    }
    
    /**
     Runs when the button is clicked, calles the manager to run the api call to update the delivery status
     
     - Author
     Robert Forbes
     
     -Date
     2017/04/20
     */
    func btnMarkDeliveredClicked() {
        _deliveryMgr.UpdateDeliveryStatus(DeliveryId: _delivery.DeliveryId!, newDeliveryStatus: "Delivered"){ (result, userMessage) in self.showCompletionMessage(result: result, userMessage: userMessage)
            self._delivery.StatusId = "Delivered"
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
