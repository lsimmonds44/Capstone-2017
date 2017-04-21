//
//  DeliveryVC.swift
//  Snack Overflow
//
//  Created by Robert Forbes on 4/20/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation
import UIKit

class DeliveryVC: UIViewController {
    let _deliveryMgr = DeliveryManager()
    var _delivery:Delivery!
    
    @IBOutlet weak var _addressLine1: UILabel!
    @IBOutlet weak var _addressLine2: UILabel!
    @IBOutlet weak var _addressCity: UILabel!
    @IBOutlet weak var _addressState: UILabel!
    @IBOutlet weak var _addressZip: UILabel!
    
    @IBOutlet weak var _btnMarkDelivered: UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        _addressLine1.text = _delivery.Address!.AddressLine1 ?? ""
        _addressLine2.text = _delivery.Address!.AddressLine2 ?? ""
        _addressCity.text = _delivery.Address!.City ?? ""
        _addressState.text = _delivery.Address!.State ?? ""
        _addressZip.text = _delivery.Address!.Zip ?? ""
        _btnMarkDelivered.addTarget(self, action: #selector(DeliveryVC.btnMarkDeliveredClicked), for: .touchUpInside)
    }
    
    func btnMarkDeliveredClicked() {
        _deliveryMgr.UpdateDeliveryStatus(DeliveryId: _delivery.DeliveryId!, newDeliveryStatus: "Delivered"){ (result, userMessage) in self.showCompletionMessage(result: result, userMessage: userMessage)
        }
    }
    
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
