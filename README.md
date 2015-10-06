
![alt tag](http://i.imgur.com/0YVZlOS.png)

# CPMCAppointmentSystem

a software solution to manage Appointments in a medical facility precisely designed for CPMC center,

This solution offre a Wpf desktop application and/or an Asp.net web application 



## The App Gobal Struct

- the app (desktop and web) are service based applications following the SOA approach, in which the main (wcf) services are :
     - The Authentification service.
     - The Main service 
     - The Medecin service 
     - The Patient service
     - others...
     - Pb: Use Multiple ServiceContracts those are used by the same service, or entirely muLtiple services !
- Wpf application (the main UI).
     -Modules (as separated User/Custom Controls): StatusBar, SideBarNavigation, ... 
- Asp.net MVC 5 ...
- ...

## TODO

- Before Demo :
     - Statistics 
     - link between the app and the sms exe
     - link status bar  
- After Demo :
     -exceptions + log.
     -audit.
     -notification + service brocker to get notified from the DB.
     -sms loop + interface [core sms, appel auto].
     -settings [theme , sms , auth services ].
     -manage passwords.
- After version beta :
     -appel vocal.
     -globalisation.
     -webcam.
     -service layer [deployement dans plus endr].
- //a recup
     -recupiration de listes de pathologies.
     -listes des specialités.
     -liste des medecins.



