USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_product_review_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_product_review_from_search
Print '' print  ' *** dropping procedure sp_retrieve_product_review_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_product_review_from_search'
GO
Create PROCEDURE sp_retrieve_product_review_from_search
(
@REVIEW_ID[INT]=NULL,
@PRODUCT_ID[INT]=NULL,
@USER_ID[INT]=NULL,
@SUPPLIER_ID[INT]=NULL,
@RATING[INT]=NULL,
@NOTES[NVARCHAR](1000)=NULL
)
AS
BEGIN
Select REVIEW_ID, PRODUCT_ID, USER_ID, SUPPLIER_ID, RATING, NOTES
FROM PRODUCT_REVIEW
WHERE (PRODUCT_REVIEW.REVIEW_ID=@REVIEW_ID OR @REVIEW_ID IS NULL)
AND (PRODUCT_REVIEW.PRODUCT_ID=@PRODUCT_ID OR @PRODUCT_ID IS NULL)
AND (PRODUCT_REVIEW.USER_ID=@USER_ID OR @USER_ID IS NULL)
AND (PRODUCT_REVIEW.SUPPLIER_ID=@SUPPLIER_ID OR @SUPPLIER_ID IS NULL)
AND (PRODUCT_REVIEW.RATING=@RATING OR @RATING IS NULL)
AND (PRODUCT_REVIEW.NOTES=@NOTES OR @NOTES IS NULL)
END