//
//  ViewController.swift
//  Snack Overflow
//
//  Created by MBPR on 3/30/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import UIKit

class LoginVC: UIViewController,UITextFieldDelegate {
    
    // Private variables
    private let _userManager = UserManager()
    private let _testUserManager = TestUserManager()
    private var _user = User(){didSet{}}
    //    {didSet{if user != nil {performSegue(withIdentifier: "HomeSeg", sender: nil)}}
    
    // outlets
    @IBOutlet weak var tfUsername: UITextField!{didSet{tfUsername.delegate = self}}
    @IBOutlet weak var tfPassword: UITextField!{didSet{tfPassword.delegate = self}}
    @IBOutlet weak var lblUserMessage: UILabel!
    @IBOutlet weak var btnLogin: UIButton!{didSet{btnLogin.layer.cornerRadius = 8}}
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: Textfield delegate that is called when the return key on the
    /// keyboard is pressed
    /// - Parameter textField: Username or Password text field
    /// - Returns:
    func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        dismissKeyboard()
        return true
    }
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: dismiss the keyboard
    func dismissKeyboard(){
        tfUsername.resignFirstResponder()
        tfPassword.resignFirstResponder()
    }
    
    /// Eric Walton
    /// 2017/04/23
    /// Description: App lifecycle handling
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
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
    /// Description: Login button that calls a validateLogin method in UserManager
    ///
    /// - Parameter sender: 
    @IBAction private func login(_ sender: UIButton) {
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









