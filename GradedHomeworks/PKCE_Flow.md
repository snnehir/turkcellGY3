## PKCE
Bir kullanýcý bir uygulamaya giriþ yapmak için 3. parti uygulamalarý (Google, Facebook, Microsoft vb.) kullandýðýnda OAuth 2.0 protokülündeki yetkilendirme kodu (authorization code) grant tipi kullanýlýr.

PKCE (Proof Key for Code Exchange), OAuth 2.0 protokolünün yetkilendirme kodu ele geçirme ([authorization code interception](https://datatracker.ietf.org/doc/html/rfc7636#section-1.1)) saldýrýlarýný önleyen bir uzantýsýdýr. 

PKCE mekanizmasý kullanýlmadan önce OAuth 2.0 protokolünün yetkilendirme sistemi þu þekilde ilerliyordu:
    ![Authorization Flow](https://mermaid.ink/img/pako:eNqVUctqwzAQ_JVlz8kP6BAozTG30Jsui7yxhe2VK61C25B_j1RjaIxL6U3M7DzQ3NCFhtGglcTvmcXx0VMbabRiZaKo3vmJROEtcVxjr4Nn0TX6krUL0X-R-iBQZNdZWR32h8MsMlXsehhC66WyM1z4LblZmcbaNX0nb50Xlxpm4FTdYaKWfxTYDjgtRX4xXGo_0_X3_t2-ioA_XEcyF_sr0jlOCTT0LLjDkeNIvimj3awAWNSOR7ZoyrOh2Nsy5r3cUdZw_hSHRmPmHeapIV3mRXOhIfH9ATHzwBk)

1. Kullanýcý bir uygulamada giriþ yapmak için istekte bulunur.
2. Uygulama, kullanýcýnýn kimliði (client identifier) ve yeniden yönlendirme uri (redirect uri) bilgileriyle kullanýcýyý yetkilendirme sunucusuna (authorization server) yönlendirir.
3. Yetkilendirme sunucusu giriþ yapma sayfasýný döndürür.
4. Kullanýcý bu sayfada bilgilerini girerek giriþ yapmak için yetkilendirme sunucusuna istekte bulunur.
5. Eðer giriþ baþarýlýysa ve kullanýcý eriþim iznini onaylarsa yetkilendirme sunucusu geçici bir yetkilendirme kodu döndürür ve kullanýcý bu kod sayesinde uygulamaya eriþim için gerekli olan access token'a sahip olur.

### Yetkilendirme Kodu Ele Geçirme Ataðý Nedir?

Kötü niyetli kullanýcý bu yetkilendirme kodu sayesinde access token alarak uygulamaya giriþ yapabilir:
![Authorization Flow](https://mermaid.ink/img/pako:eNqNkTFuwzAMRa9CcE4voCFA0I7Zgm5aCJm1BduUK1FF2yB3rxTXQOA4aDeB_Hz8XzyjCw2jwcTvmcXxi6c20mjFykRRvfMTicJr4riuPQ-eRdfVgyq5_l59yNqF6L9JfRAouI9ZU8lP-_0MMxXqehhC66V253Lpb42bFTTWDOnqaEteKHWZgWOlw0Qt3xjYXnBcjDwALmnXVuqvXsd--_9MUMeAP11HMpv7e61znBJo6FlwhyPHkXxTDnq2AmBROx7ZoinPhmJv0cql6ChrOH2JQ6Mx8w7z1JAux0fzRkPiyw_uVMtB)

### PKCE Bu Ataðý Nasýl Önler?

PKCE mekanizmasý "code_challange" ve "code_verifier" adlý 2 ek parametre ekleyerek bu saldýrýnýn önüne geçer.

code_verifier ve code challange parametreleri istemci tarafýndan üretilir.

code_verifier, kriptografik fonksiyonlarla üretilen random bir stringdir.

code_challenge, code_verifier'ýn SHA-256 hash algoritmasý kullanýlarak hashlenmiþ halidir.

![pkce](https://mermaid.ink/img/pako:eNqNksFOwzAMhl_FynnjAXKYNI3jxAW4RUJW6rWhaVJSFzamvTsOaYVUisQtsn9_-X_LV2VjRUqrgd5GCpbuHdYJOxNM6DGxs67HwPA8UFrWDt5R4GV1z4y2_a3ej9zE5D6RXQwguPeiyeTtbldgOkNtCz7WLuRuKUt_bVwvoClnGBg-HDeQc73YBr2nUFNmrSGEnA1oOOYfoccinUytf3qczf0BnDewtJcdfY9N_X-mymNAZ4kiOUq2h1jiidqdXNnjqpnt-cfMU0OAKyJBei-7eyXLwCKa1ninNqqj1KGr5D6uJgAYJf2OjNLyrDC1RplwE51w4-MlWKU5jbRRY18hz7ek9An9QLcvbprpVQ)

1. Kullanýcý code_challenge ile birlikte yetkilendirme isteðinde bulunur ve access token eriþimi isteðinde ise code_verifier kullanýr. 
2. Yetkilendirme sunucusu code_verifier'dan bir code_challenge oluþturarak ve bunu en baþtaki yetkilendirme isteðinde yer alan code_challenge ile karþýlaþtýrýr. 
3. Ýki hash deðeri eþleþirse, yetkilendirme sunucusu bir access token döndürür. 


### PKCE Mekanizmasýný Destekleyen Kimlik Saðlayýcýlarý:

1. Azure Active Directory(Azure AD)
2. Auth0
3. Okta
4. AWS Cognito
5. Google Cloud Identity Platform