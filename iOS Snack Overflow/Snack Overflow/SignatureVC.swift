//
//  SignatureVC.swift
//  Snack Overflow
//
//  Created by Robert Forbes on 4/26/17.
//  Copyright © 2017 Capstone. All rights reserved.
//

import Foundation
import UIKit

/**
 -Author
 Robert Forbes
 
 -Date
 2017/04/26
 */
protocol SignatureVCDelegate {
    func updateDelivery()
}

class SignatureVC : UIViewController{
    var delegate:SignatureVCDelegate!
    let _deliveryMgr = DeliveryManager()
    var _delivery:Delivery!
    @IBOutlet weak var _btnSubmit: UIButton!{didSet{_btnSubmit.layer.cornerRadius = 8}}
    @IBOutlet weak var _imageView: UIImageView!
    var lastPoint = CGPoint.zero
    var brushWidth: CGFloat = 5.0
    var opacity: CGFloat = 1.0
    var swiped = false
    
    

    /**
     -Author
     Robert Forbes
     
     -Date
     2017/04/26
     */
    override func viewDidLoad() {
        super.viewDidLoad()
        _btnSubmit.addTarget(self, action: #selector(self.btnSubmitClicked), for: .touchUpInside)
    }
    
    
    /**
     -Author
     Robert Forbes
     
     -Date
     2017/04/26
     */
    override func touchesBegan(_ touches: Set<UITouch>, with event: UIEvent?) {
        swiped = false
        if let touch = touches.first{
            lastPoint = touch.location(in: self.view)
        }
    }
    
    /**
     -Author
     Robert Forbes
     
     -Date
     2017/04/26
     */
    override func touchesMoved(_ touches: Set<UITouch>, with event: UIEvent?) {
        swiped = true
        if let touch = touches.first{
            let currentPoint = touch.location(in: self.view)
            drawLineFrom(fromPoint: lastPoint, toPoint: currentPoint)
            
            lastPoint = currentPoint
        }
    }
    
    /**
     -Author
     Robert Forbes
     
     -Date
     2017/04/26
     */
    override func touchesEnded(_ touches: Set<UITouch>, with event: UIEvent?) {
        if !swiped{
            drawLineFrom(fromPoint: lastPoint, toPoint: lastPoint)
        }
    }
    
    /**
        -Author
        Robert Forbes
     
        -Date
        2017/04/26
     */
    func drawLineFrom(fromPoint: CGPoint, toPoint: CGPoint){
        UIGraphicsBeginImageContext(_imageView.frame.size)
        _imageView.image?.draw(in: CGRect(x: 0, y:0, width:self.view.frame.width, height: self.view.frame.height))
        let context = UIGraphicsGetCurrentContext()
        context?.move(to: CGPoint(x:fromPoint.x, y: fromPoint.y))
        context?.addLine(to: CGPoint(x:toPoint.x, y: toPoint.y))
        context?.setBlendMode(CGBlendMode.normal)
        context?.setLineCap(CGLineCap.round)
        context?.setLineWidth(brushWidth)
        context?.setStrokeColor(UIColor(red:0, green:0, blue:0, alpha:1.0).cgColor)
        
        context?.strokePath()
        
        _imageView.image = UIGraphicsGetImageFromCurrentImageContext()
        UIGraphicsEndImageContext()
        
    }
    
    /**
     Shows an alert showing either the error message or a success message
     
     - Author
     Robert Forbes
     
     -Date
     2017/04/20
     */
    func showCompletionMessage(result:Bool, userMessage:String){
        var message = ""
        if(result == false){
            message = userMessage
        }else if(result == true){
            message = "Delivery Status Successfully updated"
            self._delivery.StatusId = "Delivered"
            self.delegate.updateDelivery()
        }
        let alertController = UIAlertController(title: "Delivery Update", message: message, preferredStyle: UIAlertControllerStyle.alert)
        alertController.addAction(UIAlertAction(title: "Dismiss", style: UIAlertActionStyle.default) { _ in
            self.navigationController?.popToViewController(self.navigationController!.viewControllers[1], animated: true)
        })
        
        
        self.present(alertController, animated: true, completion: nil)
        
        
        
    }
    
    
    /**
     
     - Author
     Robert Forbes
     
     -Date
     2017/04/26
     */
    func btnSubmitClicked(){
        let data = UIImageJPEGRepresentation(_imageView.image!, 1.0)
        let dataString = data?.base64EncodedString(options: Data.Base64EncodingOptions.lineLength64Characters)
        
        _deliveryMgr.UpdateDeliveryStatus(DeliveryId: _delivery.DeliveryId!, newDeliveryStatus: "Delivered", verificationImageStr: dataString!){ (result, userMessage) in self.showCompletionMessage(result: result, userMessage: userMessage)
            
        }
    }
    
}
