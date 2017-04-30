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
        NotificationCenter.default.addObserver(self,
                                               selector: #selector(self.keyboardNotification(notification:)),
                                               name: NSNotification.Name.UIKeyboardWillChangeFrame,
                                               object: nil)
        
        
    }
    @IBOutlet var keyboardHeightLayoutConstraint: NSLayoutConstraint?
    func keyboardNotification(notification: NSNotification) {
        if let userInfo = notification.userInfo {
            let endFrame = (userInfo[UIKeyboardFrameEndUserInfoKey] as?     NSValue)?.cgRectValue
//            let duration:TimeInterval = (userInfo[UIKeyboardAnimationDurationUserInfoKey] as? NSNumber)?.doubleValue ?? 0
            let animationCurveRawNSN = userInfo[UIKeyboardAnimationCurveUserInfoKey] as? NSNumber
            let animationCurveRaw = animationCurveRawNSN?.uintValue ?? UIViewAnimationOptions.curveEaseInOut.rawValue
            let animationCurve:UIViewAnimationOptions = UIViewAnimationOptions(rawValue: animationCurveRaw)
            if (endFrame?.origin.y)! >= keyboardHeightLayoutConstraint!.constant + endFrame!.height {
                self.keyboardHeightLayoutConstraint?.constant = 0.0
            } else {
                self.keyboardHeightLayoutConstraint?.constant = (endFrame!.size.height / 3) * -1
            }
            UIView.animate(withDuration: 0.02,
                                       delay: TimeInterval(0),
                                       options: animationCurve,
                                       animations: { self.view.layoutIfNeeded() },
                                       completion: nil)
        }
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









