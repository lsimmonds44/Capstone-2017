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
    private let _testUserManager = TestUserManager()
    private var _user = User(){didSet{}}
    //    {didSet{if user != nil {performSegue(withIdentifier: "HomeSeg", sender: nil)}}
    
    // outlets
    @IBOutlet weak var tfUsername: UITextField!{didSet{tfUsername.delegate = self}}
    @IBOutlet weak var tfPassword: UITextField!{didSet{tfPassword.delegate = self}}
    @IBOutlet weak var lblUserMessage: UILabel!
    @IBOutlet weak var btnLogin: UIButton!{didSet{btnLogin.layer.cornerRadius = 8}}
    
    
    func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        dismissKeyboard()
        return true
    }
    
    func dismissKeyboard(){
        tfUsername.resignFirstResponder()
        tfPassword.resignFirstResponder()
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
        dismissKeyboard()
        if tfUsername.text!.isEmpty || tfPassword.text!.isEmpty {
            lblUserMessage.text = "You must enter a username and a password"
        }else{
            _userManager.validateLogin(username: tfUsername.text ?? "", password: tfPassword.text ?? "") { (user,userMessage) in
                if user != nil{
                    if user?.UserId != nil{
                        DispatchQueue.main.async {
                            self.lblUserMessage.text = userMessage
                            self._user = user!
                            self.performSegue(withIdentifier: "HomeSeg", sender: nil)
                        }
                    }else{
                        DispatchQueue.main.async {
                            self.lblUserMessage.text = userMessage
                        }
                    }
                }else{
                    DispatchQueue.main.async {
                        self.lblUserMessage.text = userMessage
                    }
                }
            }
            tfUsername.text = nil
            tfPassword.text = nil
            lblUserMessage.text = ""
        }
    } // end of login button
    
    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if segue.identifier == "HomeSeg"{
            if let homeVC:HomeVC = segue.destination as? HomeVC{
                homeVC._driver = _user
            }
        }
    } // end of prepare
    
    
    
} // end of class









