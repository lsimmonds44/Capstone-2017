//
//  HomeVC.swift
//  Snack Overflow
//
//  Created by MBPR on 3/30/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import UIKit


class HomeVC: UIViewController {
    
    
    var _driver:User!
    
    @IBOutlet var btns: [UIButton]!{didSet{
        for btn in btns {
            btn.layer.cornerRadius = 8
        }
        }}
    
    
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        // Do any additional setup after loading the view.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
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
















