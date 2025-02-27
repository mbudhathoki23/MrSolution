
IF OBJECT_ID('fConvertDigit') IS NOT NULL DROP FUNCTION fConvertDigit;
GO
CREATE FUNCTION dbo.fConvertDigit(@decNumber DECIMAL)
RETURNS VARCHAR(6)
AS BEGIN
    DECLARE @strWords VARCHAR(6);
    SELECT @strWords=CASE @decNumber WHEN '1' THEN 'One'
                     WHEN '2' THEN 'Two'
                     WHEN '3' THEN 'Three'
                     WHEN '4' THEN 'Four'
                     WHEN '5' THEN 'Five'
                     WHEN '6' THEN 'Six'
                     WHEN '7' THEN 'Seven'
                     WHEN '8' THEN 'Eight'
                     WHEN '9' THEN 'Nine' ELSE '' END;
    RETURN @strWords;
END;

IF OBJECT_ID('fConvertTens') IS NOT NULL DROP FUNCTION fConvertTens;
GO
CREATE FUNCTION dbo.fConvertTens(@decNumber VARCHAR(2))
RETURNS VARCHAR(30)
AS BEGIN
    DECLARE @strWords VARCHAR(30);
    --Is value between 10 and 19?
    IF LEFT(@decNumber, 1)=1 BEGIN
        SELECT @strWords=CASE @decNumber WHEN '10' THEN 'Ten'
                         WHEN '11' THEN 'Eleven'
                         WHEN '12' THEN 'Twelve'
                         WHEN '13' THEN 'Thirteen'
                         WHEN '14' THEN 'Fourteen'
                         WHEN '15' THEN 'Fifteen'
                         WHEN '16' THEN 'Sixteen'
                         WHEN '17' THEN 'Seventeen'
                         WHEN '18' THEN 'Eighteen'
                         WHEN '19' THEN 'Nineteen' END;
    END;
    ELSE -- otherwise it's between 20 and 99.
    BEGIN
        SELECT @strWords=CASE LEFT(@decNumber, 1)WHEN '0' THEN ''
                         WHEN '2' THEN 'Twenty '
                         WHEN '3' THEN 'Thirty '
                         WHEN '4' THEN 'Forty '
                         WHEN '5' THEN 'Fifty '
                         WHEN '6' THEN 'Sixty '
                         WHEN '7' THEN 'Seventy '
                         WHEN '8' THEN 'Eighty '
                         WHEN '9' THEN 'Ninety ' END;
        SELECT @strWords=@strWords+dbo.fConvertDigit(RIGHT(@decNumber, 1));
    END;
    --Convert ones place digit.
    RETURN @strWords;
END;

IF OBJECT_ID('fNumToWords') IS NOT NULL DROP FUNCTION fNumToWords;
GO
CREATE FUNCTION dbo.fNumToWords(@decNumber DECIMAL(12, 2))
RETURNS VARCHAR(300)
AS BEGIN
    DECLARE @strNumber VARCHAR(100), @strRupees VARCHAR(200), @strPaise VARCHAR(100), @strWords VARCHAR(300), @intIndex INTEGER, @intAndFlag INTEGER;
    SELECT @strNumber=CAST(@decNumber AS VARCHAR(100));
    SELECT @intIndex=CHARINDEX('.', @strNumber);
    IF(@decNumber>99999999.99)BEGIN
        RETURN '';
    END;
    IF @intIndex>0 BEGIN
        SELECT @strPaise=dbo.fConvertTens(RIGHT(@strNumber, LEN(@strNumber)-@intIndex));
        SELECT @strNumber=SUBSTRING(@strNumber, 1, LEN(@strNumber)-3);
        IF LEN(@strPaise)>0 SELECT @strPaise=@strPaise+' paise';
    END;
    SELECT @strRupees='';
    SELECT @intIndex=LEN(@strNumber);
    SELECT @intAndFlag=2;
    WHILE(@intIndex>0)BEGIN
        IF(@intIndex=8)BEGIN
            SELECT @strRupees=@strRupees+dbo.fConvertDigit(LEFT(@decNumber, 1))+' Crore ';
            SELECT @strNumber=SUBSTRING(@strNumber, 2, LEN(@strNumber));
            SELECT @intIndex=@intIndex-1;
        END;
        ELSE IF(@intIndex=7)BEGIN
                 IF(SUBSTRING(@strNumber, 1, 1)='0')BEGIN
                     IF SUBSTRING(@strNumber, 2, 1)<>'0' BEGIN
                         IF(@strRupees<>NULL AND SUBSTRING(@strNumber, 3, 1)='0' AND SUBSTRING(@strNumber, 4, 1)='0' AND SUBSTRING(@strNumber, 5, 1)='0' AND SUBSTRING(@strNumber, 6, 1)='0' AND SUBSTRING(@strNumber, 7, 1)='0' AND @intAndFlag=2 AND @strPaise=NULL)BEGIN
                             SELECT @strRupees=@strRupees+' and '+dbo.fConvertDigit(SUBSTRING(@strNumber, 2, 1))+' Lakh ';
                             SELECT @intAndFlag=1;
                         END;
                         ELSE BEGIN
                             SELECT @strRupees=@strRupees+dbo.fConvertDigit(SUBSTRING(@strNumber, 2, 1))+' Lakh ';
                         END;
                         SELECT @strNumber=SUBSTRING(@strNumber, 3, LEN(@strNumber));
                         SELECT @intIndex=@intIndex-2;
                     END;
                     ELSE BEGIN
                         SELECT @strNumber=SUBSTRING(@strNumber, 3, LEN(@strNumber));
                         SELECT @intIndex=@intIndex-2;
                     END;
                 END;
                 ELSE BEGIN
                     IF(SUBSTRING(@strNumber, 3, 1)='0' AND SUBSTRING(@strNumber, 4, 1)='0' AND SUBSTRING(@strNumber, 5, 1)='0' AND SUBSTRING(@strNumber, 6, 1)='0' AND SUBSTRING(@strNumber, 7, 1)='0' AND @intAndFlag=2 AND @strPaise='')BEGIN
                         SELECT @strRupees=@strRupees+' and '+dbo.fConvertTens(SUBSTRING(@strNumber, 1, 2))+' Lakhs ';
                         SELECT @intAndFlag=1;
                     END;
                     ELSE BEGIN
                         SELECT @strRupees=@strRupees+dbo.fConvertTens(SUBSTRING(@strNumber, 1, 2))+' Lakhs ';
                     END;
                     SELECT @strNumber=SUBSTRING(@strNumber, 3, LEN(@strNumber));
                     SELECT @intIndex=@intIndex-2;
                 END;
        END;
        ELSE IF(@intIndex=6)BEGIN
                 IF(SUBSTRING(@strNumber, 2, 1)<>'0' OR SUBSTRING(@strNumber, 3, 1)<>'0' AND SUBSTRING(@strNumber, 4, 1)='0' AND SUBSTRING(@strNumber, 5, 1)='0' AND SUBSTRING(@strNumber, 6, 1)='0' AND @intAndFlag=2 AND @strPaise='')BEGIN
                     IF LEN(@strRupees)<=0 BEGIN
                         IF CONVERT(INT, SUBSTRING(@strNumber, 1, 1))=1 BEGIN
                             SELECT @strRupees=@strRupees+''+dbo.fConvertDigit(SUBSTRING(@strNumber, 1, 1))+' Lakh ';
                             SELECT @intAndFlag=2;
                         END;
                         ELSE BEGIN
                             SELECT @strRupees=@strRupees+''+dbo.fConvertDigit(SUBSTRING(@strNumber, 1, 1))+' Lakhs ';
                             SELECT @intAndFlag=2;
                         END;
                     END;
                     ELSE BEGIN
                         IF CONVERT(INT, SUBSTRING(@strNumber, 1, 1))=1 BEGIN
                             SELECT @strRupees=@strRupees+' and'+dbo.fConvertDigit(SUBSTRING(@strNumber, 1, 1))+' Lakh ';
                             SELECT @intAndFlag=1;
                         END;
                         ELSE BEGIN
                             SELECT @strRupees=@strRupees+' and'+dbo.fConvertDigit(SUBSTRING(@strNumber, 1, 1))+' Lakhs ';
                             SELECT @intAndFlag=1;
                         END;
                     END;
                 END;
                 ELSE BEGIN
                     IF CONVERT(INT, SUBSTRING(@strNumber, 1, 1))=1 BEGIN
                         SELECT @strRupees=@strRupees+dbo.fConvertDigit(SUBSTRING(@strNumber, 1, 1))+' Lakh ';
                     END;
                     ELSE BEGIN
                         SELECT @strRupees=@strRupees+dbo.fConvertDigit(SUBSTRING(@strNumber, 1, 1))+' Lakhs ';
                     END;
                 END;
                 SELECT @strNumber=SUBSTRING(@strNumber, 2, LEN(@strNumber));
                 SELECT @intIndex=@intIndex-1;
        END;
        ELSE IF(@intIndex=5)BEGIN
                 IF(SUBSTRING(@strNumber, 1, 1)='0')BEGIN
                     IF SUBSTRING(@strNumber, 2, 1)<>'0' BEGIN
                         IF(SUBSTRING(@strNumber, 3, 1)='0' AND SUBSTRING(@strNumber, 4, 1)='0' AND SUBSTRING(@strNumber, 5, 1)='0' AND @intAndFlag=2 AND @strPaise='')BEGIN
                             SELECT @strRupees=@strRupees+' and '+dbo.fConvertDigit(SUBSTRING(@strNumber, 2, 1))+' Thousand ';
                             SELECT @intAndFlag=1;
                         END;
                         ELSE BEGIN
                             SELECT @strRupees=@strRupees+dbo.fConvertDigit(SUBSTRING(@strNumber, 2, 1))+' Thousand ';
                         END;
                         SELECT @strNumber=SUBSTRING(@strNumber, 3, LEN(@strNumber));
                         SELECT @intIndex=@intIndex-2;
                     END;
                     ELSE BEGIN
                         SELECT @strNumber=SUBSTRING(@strNumber, 3, LEN(@strNumber));
                         SELECT @intIndex=@intIndex-2;
                     END;
                 END;
                 ELSE BEGIN
                     IF(SUBSTRING(@strNumber, 3, 1)='0' AND SUBSTRING(@strNumber, 4, 1)='0' AND SUBSTRING(@strNumber, 5, 1)='0' AND @intAndFlag=2 AND @strPaise='')BEGIN
                         --Select @strRupees=@strRupees+'andjo'+dbo.fConvertTens(substring(@strNumber,1,2))+' Thousand '
                         SELECT @strRupees=@strRupees+dbo.fConvertTens(SUBSTRING(@strNumber, 1, 2))+' Thousand ';
                         SELECT @intAndFlag=1;
                     END;
                     ELSE BEGIN
                         SELECT @strRupees=@strRupees+dbo.fConvertTens(SUBSTRING(@strNumber, 1, 2))+' Thousand ';
                     END;
                     SELECT @strNumber=SUBSTRING(@strNumber, 3, LEN(@strNumber));
                     SELECT @intIndex=@intIndex-2;
                 END;
        END;
        ELSE IF(@intIndex=4)BEGIN
                 IF((SUBSTRING(@strNumber, 3, 1)<>'0' OR SUBSTRING(@strNumber, 4, 1)<>'0')AND SUBSTRING(@strNumber, 2, 1)='0' AND @intAndFlag=2 AND @strPaise='')BEGIN
                     SELECT @strRupees=@strRupees+' and'+dbo.fConvertDigit(SUBSTRING(@strNumber, 1, 1))+' Thousand ';
                     SELECT @intAndFlag=1;
                 END;
                 ELSE BEGIN
                     SELECT @strRupees=@strRupees+dbo.fConvertDigit(SUBSTRING(@strNumber, 1, 1))+' Thousand ';
                 END;
                 SELECT @strNumber=SUBSTRING(@strNumber, 2, LEN(@strNumber));
                 SELECT @intIndex=@intIndex-1;
        END;
        ELSE IF(@intIndex=3)BEGIN
                 IF SUBSTRING(@strNumber, 1, 1)<>'0' BEGIN
                     SELECT @strRupees=@strRupees+dbo.fConvertDigit(SUBSTRING(@strNumber, 1, 1))+' Hundred ';
                     SELECT @strNumber=SUBSTRING(@strNumber, 2, LEN(@strNumber));
                     IF((SUBSTRING(@strNumber, 1, 1)<>'0' OR SUBSTRING(@strNumber, 2, 1)<>'0')AND @intAndFlag=2)BEGIN
                         SELECT @strRupees=@strRupees+' and ';
                         SELECT @intAndFlag=1;
                     END;
                     SELECT @intIndex=@intIndex-1;
                 END;
                 ELSE BEGIN
                     SELECT @strNumber=SUBSTRING(@strNumber, 2, LEN(@strNumber));
                     SELECT @intIndex=@intIndex-1;
                 END;
        END;
        ELSE IF(@intIndex=2)BEGIN
                 IF SUBSTRING(@strNumber, 1, 1)<>'0' BEGIN
                     SELECT @strRupees=@strRupees+dbo.fConvertTens(SUBSTRING(@strNumber, 1, 2));
                     SELECT @intIndex=@intIndex-2;
                 END;
                 ELSE BEGIN
                     SELECT @intIndex=@intIndex-1;
                 END;
        END;
        ELSE IF(@intIndex=1)BEGIN
                 IF(@strNumber<>'0')BEGIN
                     SELECT @strRupees=@strRupees+dbo.fConvertDigit(@strNumber);
                 END;
                 SELECT @intIndex=@intIndex-1;
        END;
        CONTINUE;
    END;
    IF LEN(@strRupees)>0 SELECT @strRupees=@strRupees+' rupees ';
    IF(LEN(@strPaise)<>0)BEGIN
        IF LEN(@strRupees)>0 SELECT @strRupees=@strRupees+' and ';
    END;
    SELECT @strWords=ISNULL(@strRupees, '')+ISNULL(@strPaise, '');
    SELECT @strWords=@strWords+' only';
    RETURN @strWords;
END;