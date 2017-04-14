//
//  ViewController.swift
//  Snack Overflow
//
//  Created by MBPR on 3/30/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import UIKit

class LoginVC: UIViewController {
    
    // variables
    private let _userManager = UserManager()
    private let _testUserManager = UserManager()
    private var user = User()
//    {didSet{if user != nil {performSegue(withIdentifier: "HomeSeg", sender: nil)}}
    
    // outlets
    @IBOutlet weak var tfUsername: UITextField!
    @IBOutlet weak var tfPassword: UITextField!
    
    
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    @IBAction func login(_ sender: UIButton) {
        _testUserManager.validateLogin(username: tfUsername.text ?? "", password: tfPassword.text ?? "") { (user) in
            
            if user?.UserId != nil{
                DispatchQueue.main.async {
                    self.performSegue(withIdentifier: "HomeSeg", sender: nil)
                }
            }
        }
        tfUsername.text = nil
        tfPassword.text = nil
    }


}

