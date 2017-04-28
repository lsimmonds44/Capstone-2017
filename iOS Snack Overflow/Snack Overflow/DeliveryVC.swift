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


class DeliveryVC: UIViewController,UITableViewDataSource,UITableViewDelegate,SignatureVCDelegate {
    
    
    var delegate:DeliveryVCDelegate!
    var _delivery:Delivery!
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
        //Address line 2 was causing problems when creating a link to navigation
        //let addLine2 = address.AddressLine2 ?? ""
        let addCity = address.City ?? ""
        let addState = address.State ?? ""
        let addZip = address.Zip ?? ""
        _txtAddress.text = addLine1 + ", \n" + addCity + ", " + addState + ", " + addZip
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
        
    }
    
    /**
     Runs when the button is clicked, calls the manager to run the api call to update the delivery status
     
     - Author
     Robert Forbes
     
     -Date
     2017/04/26
     */
    func updateDelivery() {
        self._delivery.StatusId = "Delivered"
        self.delegate.updatePin()
    }
    
    
    /*
     // MARK: - Navigation
     
     // In a storyboard-based application, you will often want to do a little preparation before navigation
     override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
     // Get the new view controller using segue.destinationViewController.
     // Pass the selected object to the new view controller.
     }
     */
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if segue.identifier == "SignatureSeg"{
            if let signatureVC:SignatureVC = segue.destination as? SignatureVC{
                signatureVC._delivery = _delivery
                signatureVC.delegate = self
            }
        }
    }
    
    
}
