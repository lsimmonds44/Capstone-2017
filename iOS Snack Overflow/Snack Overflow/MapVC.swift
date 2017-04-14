//
//  MapVC.swift
//  Snack Overflow
//
//  Created by MBPR on 4/14/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import UIKit
import MapKit

class MapVC: UIViewController,MKMapViewDelegate,CLLocationManagerDelegate {

    // outlets
    private var _locationManager = CLLocationManager()
    @IBOutlet weak var map: MKMapView!{didSet{
        map.delegate = self
        map.mapType = .hybrid
        map.showsUserLocation = true
        map.setUserTrackingMode(MKUserTrackingMode.follow, animated: true)
        _locationManager.delegate = self
        map.isRotateEnabled = false
        _locationManager.requestAlwaysAuthorization()
        _locationManager.desiredAccuracy = kCLLocationAccuracyBest
        _locationManager.startUpdatingLocation()
        }}
    
    
    override func viewDidLoad() {
        super.viewDidLoad()

        // Do any additional setup after loading the view.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    

    /*
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        // Get the new view controller using segue.destinationViewController.
        // Pass the selected object to the new view controller.
    }
    */

}
