//
//  ViewController.swift
//  Snack Overflow
//
//  Created by MBPR on 3/30/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import UIKit

class LoginVC: UIViewController,UITextFieldDelegate {
    
    // variables
    private let _userManager = UserManager()
    private let _testUserManager = UserManager()
    private var user = User()
    //    {didSet{if user != nil {performSegue(withIdentifier: "HomeSeg", sender: nil)}}
    
    // outlets
    @IBOutlet weak var tfUsername: UITextField!{didSet{tfUsername.delegate = self}}
    @IBOutlet weak var tfPassword: UITextField!{didSet{tfPassword.delegate = self}}
    @IBOutlet weak var lblUserMessage: UILabel!
    
    
    func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        textField.resignFirstResponder()
        
        return true
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    @IBAction func login(_ sender: UIButton) {
        if tfUsername.text!.isEmpty || tfPassword.text!.isEmpty {
            lblUserMessage.text = "You must enter a username and a password"
        }else{
            _testUserManager.validateLogin(username: tfUsername.text ?? "", password: tfPassword.text ?? "") { (user) in
                if user != nil{
                    if user?.UserId != nil{
                        DispatchQueue.main.async {
                            self.lblUserMessage.text = ""
                            self.performSegue(withIdentifier: "HomeSeg", sender: nil)
                        }
                    }else{
                        DispatchQueue.main.async {
                            self.lblUserMessage.text = "Error connecting to database. Check for data connection. If data is present and still can't connect. Try again later."
                        }
                    }
                }else{
                    DispatchQueue.main.async {
                        self.lblUserMessage.text = "Username or Password incorrect"
                    }
                }
            }
            tfUsername.text = nil
            tfPassword.text = nil
            lblUserMessage.text = ""
        }
    } // end of login button
    
    
} // end of class

