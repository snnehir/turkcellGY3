## PKCE
Bir kullan�c� bir uygulamaya giri� yapmak i�in 3. parti uygulamalar� (Google, Facebook, Microsoft vb.) kulland���nda OAuth 2.0 protok�l�ndeki yetkilendirme kodu (authorization code) grant tipi kullan�l�r.

PKCE (Proof Key for Code Exchange), OAuth 2.0 protokol�n�n yetkilendirme kodu ele ge�irme ([authorization code interception](https://datatracker.ietf.org/doc/html/rfc7636#section-1.1)) sald�r�lar�n� �nleyen bir uzant�s�d�r. 

PKCE mekanizmas� kullan�lmadan �nce OAuth 2.0 protokol�n�n yetkilendirme sistemi �u �ekilde ilerliyordu:
    ![Authorization Flow](https://mermaid.ink/img/pako:eNqVUctqwzAQ_JVlz8kP6BAozTG30Jsui7yxhe2VK61C25B_j1RjaIxL6U3M7DzQ3NCFhtGglcTvmcXx0VMbabRiZaKo3vmJROEtcVxjr4Nn0TX6krUL0X-R-iBQZNdZWR32h8MsMlXsehhC66WyM1z4LblZmcbaNX0nb50Xlxpm4FTdYaKWfxTYDjgtRX4xXGo_0_X3_t2-ioA_XEcyF_sr0jlOCTT0LLjDkeNIvimj3awAWNSOR7ZoyrOh2Nsy5r3cUdZw_hSHRmPmHeapIV3mRXOhIfH9ATHzwBk)

1. Kullan�c� bir uygulamada giri� yapmak i�in istekte bulunur.
2. Uygulama, kullan�c�n�n kimli�i (client identifier) ve yeniden y�nlendirme uri (redirect uri) bilgileriyle kullan�c�y� yetkilendirme sunucusuna (authorization server) y�nlendirir.
3. Yetkilendirme sunucusu giri� yapma sayfas�n� d�nd�r�r.
4. Kullan�c� bu sayfada bilgilerini girerek giri� yapmak i�in yetkilendirme sunucusuna istekte bulunur.
5. E�er giri� ba�ar�l�ysa ve kullan�c� eri�im iznini onaylarsa yetkilendirme sunucusu ge�ici bir yetkilendirme kodu d�nd�r�r ve kullan�c� bu kod sayesinde uygulamaya eri�im i�in gerekli olan access token'a sahip olur.

### Yetkilendirme Kodu Ele Ge�irme Ata�� Nedir?

K�t� niyetli kullan�c� bu yetkilendirme kodu sayesinde access token alarak uygulamaya giri� yapabilir:
![Authorization Flow](https://mermaid.ink/img/pako:eNqNkTFuwzAMRa9CcE4voCFA0I7Zgm5aCJm1BduUK1FF2yB3rxTXQOA4aDeB_Hz8XzyjCw2jwcTvmcXxi6c20mjFykRRvfMTicJr4riuPQ-eRdfVgyq5_l59yNqF6L9JfRAouI9ZU8lP-_0MMxXqehhC66V253Lpb42bFTTWDOnqaEteKHWZgWOlw0Qt3xjYXnBcjDwALmnXVuqvXsd--_9MUMeAP11HMpv7e61znBJo6FlwhyPHkXxTDnq2AmBROx7ZoinPhmJv0cql6ChrOH2JQ6Mx8w7z1JAux0fzRkPiyw_uVMtB)

### PKCE Bu Ata�� Nas�l �nler?

PKCE mekanizmas� "code_challange" ve "code_verifier" adl� 2 ek parametre ekleyerek bu sald�r�n�n �n�ne ge�er.

code_verifier ve code challange parametreleri istemci taraf�ndan �retilir.

code_verifier, kriptografik fonksiyonlarla �retilen random bir stringdir.

code_challenge, code_verifier'�n SHA-256 hash algoritmas� kullan�larak hashlenmi� halidir.

![pkce](https://mermaid.ink/img/pako:eNqNksFOwzAMhl_FynnjAXKYNI3jxAW4RUJW6rWhaVJSFzamvTsOaYVUisQtsn9_-X_LV2VjRUqrgd5GCpbuHdYJOxNM6DGxs67HwPA8UFrWDt5R4GV1z4y2_a3ej9zE5D6RXQwguPeiyeTtbldgOkNtCz7WLuRuKUt_bVwvoClnGBg-HDeQc73YBr2nUFNmrSGEnA1oOOYfoccinUytf3qczf0BnDewtJcdfY9N_X-mymNAZ4kiOUq2h1jiidqdXNnjqpnt-cfMU0OAKyJBei-7eyXLwCKa1ninNqqj1KGr5D6uJgAYJf2OjNLyrDC1RplwE51w4-MlWKU5jbRRY18hz7ek9An9QLcvbprpVQ)

1. Kullan�c� code_challenge ile birlikte yetkilendirme iste�inde bulunur ve access token eri�imi iste�inde ise code_verifier kullan�r. 
2. Yetkilendirme sunucusu code_verifier'dan bir code_challenge olu�turarak ve bunu en ba�taki yetkilendirme iste�inde yer alan code_challenge ile kar��la�t�r�r. 
3. �ki hash de�eri e�le�irse, yetkilendirme sunucusu bir access token d�nd�r�r. 


### PKCE Mekanizmas�n� Destekleyen Kimlik Sa�lay�c�lar�:

1. Azure Active Directory(Azure AD)
2. Auth0
3. Okta
4. AWS Cognito
5. Google Cloud Identity Platform