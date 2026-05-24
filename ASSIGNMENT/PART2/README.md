# CyberGuard — Cyber Awareness Chatbot

## Description
A WinForms cybersecurity awareness chatbot built in C# that educates 
users on Password Safety, Phishing, and Safe Browsing.

## Setup Instructions
1. Open Visual Studio
2. Create a Windows Forms App (.NET Framework 4.8) project
3. Replace Form files with the provided source files
4. Add greet.wav — set Copy to Output Directory = Copy Always
5. Press F5 to run

## How to Use
- Type your name when prompted
- Use the numbered menu or type freely
- Type "tell me more" for follow-up tips
- Type "exit" to end the session

## Features
- Keyword recognition (12 keywords)
- Sentiment detection (worried, confused, frustrated, curious)
- Random tips using TipProvider delegate
- Memory and recall across the session
- Voice greeting on startup
- ASCII logo display

## Project Structure
- CyberDesign.cs  — display and styling (base class)
- CyberSpace.cs   — all chatbot logic (inherits CyberDesign)
- CyberForm.cs    — WinForms UI layer
- Program.cs      — entry point

## YouTube Presentation
[Link here]

## GitHub
[Your repo link here]
