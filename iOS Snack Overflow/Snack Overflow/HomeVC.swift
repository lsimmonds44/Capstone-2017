//
//  HomeVC.swift
//  Snack Overflow
//
//  Created by MBPR on 3/30/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import UIKit


class HomeVC: UIViewController,RouteListViewDelegate {
    
    /// Eric Walton
    /// 2017/04/23
    // Private variables
    private var _routeListView = RouteListView()
   
    /// Eric Walton
    /// 2017/04/23
    // Global variables
    var _driver:User!
    var _route:Route!
    var _pickup:Pickup!
    
    /// Eric Walton
    /// 2017/04/23
    // Outlets
    @IBOutlet private weak var userMessageLbl: UILabel!{didSet{userMessageLbl?.text = "Welcome\n \(_driver.FirstName ?? "Anonymous") \(_driver.LastName ?? "???")"}}
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: Outlet collecting used to style the buttons
    @IBOutlet private var btns: [UIButton]!{didSet{
        for btn in btns {
            btn.layer.cornerRadius = 8
        }
        }}
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: Selected route delegate called from RouteListView
    /// Used to set the route and call a segue to Map view controller
    /// - Parameter route: Route object
    func RouteSelected(route: Route) {
        _routeListView.RouteListView.removeFromSuperview()
        _route = route
        self.performSegue(withIdentifier: "DeliverySeg", sender: nil)
        dismissRouteListView()
    }
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: Selected pickup delegate called from RouteListView
    /// Used to set the pickup and call a segue to Map view controller
    /// - Parameter pickup: Pickup object
    func PickupSelected(pickup: Pickup) {
        _pickup = pickup
        self.performSegue(withIdentifier: "PickupSeg", sender: nil)
        dismissRouteListView()
    }
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: Cancel button delegate called from RouteListView
    /// Used to dismiss the RouteListView without a selection being made
    func RouteSelectionCanceled() {
        dismissRouteListView()
    }
    
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: dismisses the RouteListView called from multiple methods
    func dismissRouteListView(){
        _routeListView.RouteListView.removeFromSuperview()
    }
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: App lifecycle handling
    /// edgesForExtendedLayout sets y:0 at the bottom of the navigation controller
    /// instead of the top left of the screen
    override func viewDidLoad() {
        super.viewDidLoad()
        edgesForExtendedLayout = []
        // Do any additional setup after loading the view.
    }
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: App lifecycle handling
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: The delivery and pickup buttons
    ///
    /// - Parameter sender: The delivery and pickup buttons
    @IBAction func ViewListButtons(_ sender: UIButton) {
        if sender.title(for: UIControlState.normal)!.contains("Delivery"){
            _routeListView.addView(apiToCall: "Delivery", driverId: _driver.UserId!)
            displayRouteListView()
        }else{
            _routeListView.addView(apiToCall: "Pickup", driverId: _driver.UserId!)
            displayRouteListView()
        }
    }
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: Method to display the listview for Route and Pickup
    func displayRouteListView(){
            _routeListView.RouteListView.frame = CGRect(x: 10, y: self.view.frame.midY / 3, width: self.view.bounds.size.width - 20, height: self.view.bounds.height / 2)
            _routeListView.delegate = self
            self.view.addSubview(_routeListView.RouteListView)
    }
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: Called when the device is rotated and dismisses the Route list view
    ///
    /// - Parameters:
    ///   - size:
    ///   - coordinator: 
    override func viewWillTransition(to size: CGSize, with coordinator: UIViewControllerTransitionCoordinator) {
        if _routeListView.RouteListView != nil{
            dismissRouteListView()
        }
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
















