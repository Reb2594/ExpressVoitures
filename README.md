# ExpressVoitures

## Description du projet

ExpressVoiture est une application de gestion de véhicules développée en ASP.NET Core avec Entity Framework. Elle permet aux utilisateurs d'ajouter, modifier, visualiser et supprimer des véhicules, tout en gérant les images associées et en offrant des fonctionnalités dynamiques pour la gestion des marques, modèles, finitions, etc.

Ce projet respecte les principes de l'architecture MVC (Model-View-Controller) et s'appuie sur plusieurs services pour la gestion des logiques métier et les échanges avec la base de données.

## Prérequis

Avant de cloner et exécuter ce projet, assurez-vous d'avoir installé les éléments suivants :

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (de préférence)

## Installation

1. Clonez le dépôt GitHub :

  https://github.com/Reb2594/ExpressVoitures.git

2. Restaurer les dépendances : 

Après avoir cloné le projet, vous devez restaurer les dépendances NuGet et les packages npm si nécessaire.

En tapant dotnet restore dans la console du gestionnaire de package.

3. Configurer la base de données

Par défaut, cette application utilise SQL Server LocalDB, qui fonctionne uniquement sur Windows. Si vous êtes sous Windows et que LocalDB est installé, aucune modification n'est nécessaire. Cependant :

- Si vous utilisez un autre système d'exploitation ou une instance différente de SQL Server :

Modifiez la chaîne de connexion dans appsettings.json.

Exemple pour une instance SQL Server standard :

"ConnectionStrings": {

  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=ExpressVoitures;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;MultipleActiveResultSets=true"

}

- Appliquez les migrations (dans la console du gestionnaire de package) pour créer la base de données :

dotnet ef database update

- Il suffira ensuite de lancer le projet, via IIS Express sur Chrome, Edge ou Firefox, la base sera automatiquement créée et, dans un souci d'amélioration de l'expérience utilisateur dans le cadre de la démo, celle-ci sera aussi  peuplée de quelques valeurs pour les marques, les modèles et les finitions.

## Authentification

Dès le premier lancement du projet, afin de faciliter la démo, il sera possible de se connecter en tant qu'administrateur grâce aux identifiants suivants :

**Login** : admin@example.com

**Mot de passe** : AdminPassword123!

## Utilisation

Une fois l'application lancée, vous pouvez interagir avec elle via l'interface web. Voici quelques fonctionnalités principales :

- **Page d'accueil** : Affiche la liste des véhicules enregistrés. 
Les véhicules marqués comme disponibles sont visibles par tous les utilisateurs tandis que ceux qui sont marqués comme non-disponibles ne sont visibles que pour les utilisateurs conectés en tant qu'admin.

- **Ajout d'un un véhicule** : Remplir un formulaire pour ajouter un véhicule avec des informations comme la marque, le modèle, la finition, le prix d'achat, et des images.

- **Modifier un véhicule** : Modifiez les informations d'un véhicule existant.

- **Supprimer un véhicule** : Supprimez un véhicule de la base de données.

- **Gestion des images** : Possibilité d'ajouter plusieurs images pour chaque véhicule.

- **Utilisation d'ASP.NET Core Identity** : Authentification des administrateurs avec un compte préenregistré. Accès restreint aux actions d'édition et de suppression des véhicules.

- **Gestion des erreurs** : Affichage de messages d'erreur et de succès pour améliorer l'expérience utilisateur.
  
## Architecture

Le projet utilise une architecture MVC et est basée sur l'utilisation de services avec les composants suivants :

- **Models** : Contiennent les entités représentant les données de l'application (par ex. : `Vehicule`, `Modele`, `Marque`, `Reparation`, etc.).

- **ViewModels** : Facilitent la présentation des données dans les vues.

- **Services** : Logique métier encapsulée dans des services qui gèrent les interactions avec la base de données (par ex. : `VehiculeService`, `ImageService`, `AnneeService`).

- **Controllers** : Gèrent les requêtes HTTP et orchestrent les interactions entre les services, les modèles et les vues.

## Auteure

Rebecca Bajazet


