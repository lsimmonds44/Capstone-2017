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
    var _route:Route!
    var _pickup:Pickup!
    
    @IBOutlet weak var userMessageLbl: UILabel!{didSet{userMessageLbl?.text = "Welcome\n \(_driver.FirstName ?? "Anonymous") \(_driver.LastName ?? "???")"}}
    
    @IBOutlet var btns: [UIButton]!{didSet{
        for btn in btns {
            btn.layer.cornerRadius = 8
        }
        }}
    
    func RouteSelected(route: Route) {
        _routeListView.RouteListView.removeFromSuperview()
        _route = route
        self.performSegue(withIdentifier: "DeliverySeg", sender: nil)
        dismissRouteListView()
    }
    
    func PickupSelected(pickup: Pickup) {
        _pickup = pickup
        self.performSegue(withIdentifier: "PickupSeg", sender: nil)
        dismissRouteListView()
    }
    
    func RouteSelectionCanceled() {
        dismissRouteListView()
    }
    
    func dismissRouteListView(){
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
            displayRouteListView()
        }else{
            _routeListView.addView(apiToCall: "Pickup", driverId: _driver.UserId!)
            displayRouteListView()
        }
    }
    
    func displayRouteListView(){
            _routeListView.RouteListView.frame = CGRect(x: 10, y: self.view.frame.midY / 3, width: self.view.bounds.size.width - 20, height: _routeListView.RouteListView.bounds.height)
            _routeListView.delegate = self
            self.view.addSubview(_routeListView.RouteListView)
    }
    
    
    // MARK: - Navigation
    
    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if segue.identifier == "DeliverySeg"{
            if let mapVC:MapVC = segue.destination as? MapVC{
                mapVC.navigationItem.title = "Deliveries"
                mapVC._driver = _driver
                mapVC._route = _route
            }
        }else if segue.identifier == "PickupSeg"{
            if let mapVC:MapVC = segue.destination as? MapVC{
                mapVC.navigationItem.title = "Pick-Ups"
                mapVC._driver = _driver
                mapVC._pickup = _pickup
            }
        }
    } // end of prepare
    
    
} // end of class
















