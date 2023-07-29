# University Management System (UMS)

## Overview

This API was made as a project for the backend module at inmind.ai and was designed with a Domain-Driven Design (DDD) architecture with Schema-Based Multitenancy and using a Repository Design Pattern. The application is developed using ASP.NET Core 6, Entity Framework Core, MediatR for CQRS, and Firebase for email and authentication/authorization services.

**Note: Due to time constraints (caused by work commitments) and underestimating the implementation complexity, the microservices architecture was not implemented. However, I made sure to grasp the necessary knowledge and concepts for future expansion to microservices. If you could please take that into consideration.**

## Features

### 1. Schema-Based Multitenancy:
- Schema-based multitenancy was implemented by saving the branchid for a user inside the JWT claim that is returned by the **login** endpoint, the id is extracted from the claim through a middleware **after** authentication.

### 2. Role-Based Authentication and Authorization
- Implemented Firebase Authentication for secure user authentication and user role management.
- Role-based authorization to restrict access to certain functionalities based on user roles.
- Implemented a signup and login endpoint that returns a JWT token.

### 3. Admin Functionalities
- Admins can create and delete courses.
- Get endpoints for getting 2 teachers' common students and getting the gender distribution per course.

### 4. Student Functionalities
- Students can enroll in available courses.
- Students receive an email notification upon course enrollment (using Firebase Cloud Messaging).

### 5. Teacher Functionalities
- Add Session Time
- Add Teacher Per Course
- Add Teacher Per Course Per Session Time

### 6. General Endpoints:
- Get endpoints for getting course by its id and getting branchid.
- Implemented OData for Get All Courses endpoint.

## Future Development (Microservices)

Refactoring the functionalities into logical microservices (such as authentication, student, admin, teacher functionalities) while using RabbitMQ for interservice communication and centralizing the API entry point through a common gateway.

### Thank you for the module it was great :)
