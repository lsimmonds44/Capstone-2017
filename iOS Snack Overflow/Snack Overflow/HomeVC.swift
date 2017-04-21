//
//  HomeVC.swift
//  Snack Overflow
//
//  Created by MBPR on 3/30/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import UIKit


class HomeVC: UIViewController,RouteListViewDelegate {
    
    
    var _driver:User!
    var _routeListView = RouteListView()
    
    @IBOutlet var btns: [UIButton]!{didSet{
        for btn in btns {
            btn.layer.cornerRadius = 8
        }
        }}
    
    func RouteSelected(route: Route) {
        _routeListView.RouteListView.removeFromSuperview()
        self.performSegue(withIdentifier: "DeliverySeg", sender: nil)
    }
    
    func RouteSelectionCanceled() {
        _routeListView.RouteListView.removeFromSuperview()
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        edgesForExtendedLayout = []
        // Do any additional setup after loading the view.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    @IBAction func ViewListButtons(_ sender: UIButton) {
        if sender.title(for: UIControlState.normal)!.contains("Delivery"){
            _routeListView.addView(apiToCall: "Delivery", driverId: _driver.UserId!)
            _routeListView.RouteListView.frame = CGRect(x: 0, y: 0, width: self.view.bounds.size.width, height: _routeListView.RouteListView.bounds.height)
            _routeListView.delegate = self
            self.view.addSubview(_routeListView.RouteListView)
        }else{
            
        }
    }
    
    
    
    
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if segue.identifier == "DeliverySeg"{
            if let mapVC:MapVC = segue.destination as? MapVC{
                mapVC.navigationItem.title = "Deliveries"
                mapVC._driver = _driver
            }
        }else if segue.identifier == "PickupSeg"{
            if let mapVC:MapVC = segue.destination as? MapVC{
                mapVC.navigationItem.title = "Pick-Ups"
                mapVC._driver = _driver
            }
        }
    } // end of prepare
 

} // end of class
















