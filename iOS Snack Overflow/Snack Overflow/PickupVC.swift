//
//  PickupVC.swift
//  Snack Overflow
//
//  Created by Robert Forbes on 4/23/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation
import UIKit

protocol PickupVCDelegate {
    func updatePin()
}

class PickupVC : UIViewController,UITableViewDataSource,UITableViewDelegate{
    let _pickupMgr = PickupManager()
    var _pickup:Pickup!
    
    @IBOutlet weak var _txtAddress: UITextView!
    @IBOutlet weak var _btnMarkPickedUp: UIButton!
    @IBOutlet weak var _tblProducts: UITableView!
    var delegate:PickupVCDelegate!
    
    /**
     - Author
     Robert Forbes
     
     -Date
     2017/04/23
     */
    override func viewDidLoad() {
        super.viewDidLoad()
        let address = _pickup.Address! 
        let addLine1 = address.AddressLine1 ?? ""
        let addLine2 = address.AddressLine2 ?? ""
        let addCity = address.City ?? ""
        let addState = address.State ?? ""
        let addZip = address.Zip ?? ""
        _txtAddress.text = addLine1 + ", " + addLine2 + "\n" + addCity + ", " + addState + ", " + addZip
        _btnMarkPickedUp.addTarget(self, action: #selector(PickupVC.btnMarkPickedUpClicked), for: .touchUpInside)
        
        _tblProducts.dataSource = self
        _tblProducts.delegate = self
    }
    
    /**
     - Author
     Robert Forbes
     
     -Date
     2017/04/23
     */
    func tableView(_ tableView: UITableView, numberOfRowsInSection section:Int) -> Int{
        return _pickup.PickupLineList.count
        
    }
    
    /**
     - Author
     Robert Forbes
     
     -Date
     2017/04/23
     */
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let product = _pickup.PickupLineList[indexPath.row]
        let cell = UITableViewCell(style: .subtitle, reuseIdentifier: "PickupCell")
        cell.textLabel?.text = product.productName
        
        cell.detailTextLabel?.text = "Quantity: " + String(product.Quantity!)
        return cell
    }
    
    /**
     - Author
     Robert Forbes
     
     -Date
     2017/04/23
     */
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return 50
    }
    
    /**
     - Author
     Robert Forbes
     
     -Date
     2017/04/23
     */
    func tableView(_ tableView: UITableView, estimatedHeightForRowAt indexPath: IndexPath) -> CGFloat {
        return 50
    }
    
    /**
     Runs when the button is clicked, calls the manager to run the api call to update the pickup status
     
     - Author
     Robert Forbes
     
     -Date
     2017/04/23
     */
    func btnMarkPickedUpClicked() {
        _pickupMgr.UpdatePickupStatus(pickup: _pickup){ (result, userMessage) in self.showCompletionMessage(result: result, userMessage: userMessage)
            for line in self._pickup.PickupLineList{
                line.PickupStatus = true
            }
            self.delegate.updatePin()
        }
    }
    
    /**
     Shows an alert showing either the error message or a success message
     
     - Author
     Robert Forbes
     
     -Date
     2017/04/23
     */
    func showCompletionMessage(result:Bool, userMessage:String){
        var message = ""
        if(result == false){
            message = userMessage
        }else if(result == true){
            message = "Pickup Status Successfully updated"
        }
        let alertController = UIAlertController(title: "Pickup Update", message: message, preferredStyle: UIAlertControllerStyle.alert)
        alertController.addAction(UIAlertAction(title: "Dismiss", style: UIAlertActionStyle.default,handler: nil))
        
        self.present(alertController, animated: true, completion: nil)
    }
}
