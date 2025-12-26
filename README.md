# Google Calendar Integration – Console App (.NET)

This project demonstrates how to integrate **Google Calendar** in a **.NET Console Application** to:

* Create calendar events
* Send email invitations
* Generate Google Meet links (for Telehealth appointments)

The invitation is sent **from a Google account (sender)** to another email **(receiver / doctor)**.

---

## 🧩 Project Overview

The application:

* Authenticates using **Google OAuth 2.0 (Desktop App)**
* Uses **Google Calendar API**
* Creates calendar appointments programmatically
* Optionally generates a **Google Meet** link

---

## 🛠 Prerequisites

Before starting, make sure you have:

* .NET 6 or later
* A Google account (company or system email)
* Access to **Google Cloud Console**
* Internet connection (for OAuth & API calls)

---

## ☁️ Google Cloud Setup (Step by Step)

### 1️⃣ Create a Google Cloud Project

1. Go to **Google Cloud Console**
2. Click **Select Project → New Project**
3. Name it (e.g. `Company-Calendar-PoC`)
4. Click **Create**

---

### 2️⃣ Enable Google Calendar API

1. Open your project
2. Go to **APIs & Services → Library**
3. Search for **Google Calendar API**
4. Click **Enable**

---

### 3️⃣ Configure OAuth Consent Screen

1. Go to **Google Auth Platform / OAuth consent**
2. Click **Get Started**
3. Choose **External**
4. Fill required fields:

   * App name
   * User support email
   * Developer contact email
5. Save and continue

#### Scopes

Add:

```
https://www.googleapis.com/auth/calendar
```

#### Test Users

Add the **sender email** (Google account that will create events), e.g.:

```
mahmoudahmed10197@gmail.com
```

Save changes.

> ⏱ Note: It may take a few minutes for changes to take effect.

---

### 4️⃣ Create OAuth Client (Desktop App)

1. Go to **APIs & Services → Credentials**
2. Click **Create Credentials → OAuth Client ID**
3. Choose **Desktop application**
4. Name it (e.g. `Calendar Console App`)
5. Click **Create**

---

### 5️⃣ Download credentials.json

After creating the client:

1. Go to **Credentials**
2. Find your OAuth Client
3. Click **Download JSON**
4. Rename the file to:

```
credentials.json
```

5. Place it in the **root directory** of the console application

---

## 📦 NuGet Packages

Install the following packages:

```
Google.Apis.Calendar.v3
Google.Apis.Auth
```

---

## 🔐 Authentication Flow (How It Works)

* On first run:

  * Browser opens
  * User logs in with **sender Google account**
  * Permissions are granted
* A `token.json` file is created locally
* Next runs reuse the token (no login required)

---

## 🧠 Important Concept: Sender vs Receiver

| Role     | Who?                                          |
| -------- | --------------------------------------------- |
| Sender   | Google account used in OAuth (calendar owner) |
| Receiver | Attendee email (doctor / user)                |

The **sender must be added as a Test User** in Google Cloud.

---

## 🧪 Running the Application

### Example Code Usage

```csharp
var calendarService = new GoogleCalendarService();

var appointment = new AppointmentRequest
{
    DoctorEmail = "doctor@example.com",
    PatientName = "Ahmed Ali",
    AppointmentType = "Telehealth Consultation",
    Description = "Initial patient consultation",
    StartUtc = DateTime.UtcNow.AddHours(1),
    EndUtc = DateTime.UtcNow.AddHours(2),
    IsTeleHealth = true
};

var meetingLink = await calendarService.CreateAppointmentAsync(appointment);
```

---

## 📅 What Happens When You Run It?

* Event is created in **sender's Google Calendar**
* Invitation email is sent to the doctor
* Google Meet link is generated (if Telehealth)
* Meet link can be saved in database

---

## ❗ Common Issues & Fixes

### "access_denied" error

* Sender email not added as Test User

### credentials.json not found

* File must be in the app root directory

### Changes not applied

* Wait 5–10 minutes after Google Cloud changes

---

## 🚀 Production Notes

* For production:

  * Submit app for Google verification
  * Use Workspace account if possible
  * Secure token storage

---

## ✅ Summary

* Uses OAuth 2.0 Desktop Flow
* Sends calendar invites programmatically
* Supports Google Meet creation
* Ideal for scheduling & telehealth systems

---

Happy Coding 🚀
