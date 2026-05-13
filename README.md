# Reading Club

Gustavo has a large comic book collection, so he decided to lend them to his friends.  
That is how the Reading Club was created.

To avoid losing any comic books, his father hired the students from the Programming Academy to build an application capable of managing comic books and controlling loans.

## 1. Boxes Module

### Functional Requirements

- The system must allow registering new boxes
- The system must allow editing existing boxes
- The system must allow deleting boxes
- The system must allow viewing all boxes

### Business Rules

- Required fields:
  - Label (unique text, maximum 50 characters)
  - Color (palette selection or hexadecimal value)
  - Loan days (number, default value: 7)
- Duplicate labels are not allowed
- A box cannot be deleted if it contains linked comic books
- Each box defines the maximum loan period for its comic books

## 2. Comic Books Module

### Functional Requirements

- The system must allow registering new comic books
- The system must allow editing existing comic books
- The system must allow deleting comic books
- The system must allow viewing all comic books

### Business Rules

- Required fields:
  - Title (2-100 characters)
  - Issue number (positive number)
  - Publication year (valid date)
  - Box (required selection)
- Comic books cannot share the same title and issue number

## 3. Friends Module

### Functional Requirements

- The system must allow adding new friends
- The system must allow editing registered friends
- The system must allow deleting registered friends
- The system must allow viewing registered friends

### Business Rules

- Required fields:
  - Name (minimum 3 characters, maximum 100)
  - Guardian name (minimum 3 characters, maximum 100)
  - Phone number (validated format: 10-11 digits)
- Friends cannot share the same name and phone number

## 4. Loans Module

### Functional Requirements

- The system must allow registering new loans
- The system must allow registering returns
- The system must allow viewing open and closed loans

### Refactorings

#### Comic Books

- The system must store and display the current status of comic books (available/loaned/reserved)

#### Friends

- The system must allow viewing loans from specific friends
- A friend cannot be deleted if they have linked loans

### Business Rules

- Required fields:
  - Friend
  - Comic book (must be available)
  - Loan date (automatic)
  - Return date (calculated according to the box)
- Possible statuses: Open / Completed / Late
- Each friend can only have one active loan at a time
- Late loans must be visually highlighted
- The return date must be automatically calculated (loan date + box loan days)

## How to Use

1. Clone the repository or download the source code.
2. Open the terminal or command prompt and navigate to the root folder.
3. Run the command below to restore the project dependencies.

```bash
dotnet restore
```

4. Run the project with real-time compilation.

```bash
dotnet run --project ReadingClub.ConsoleApp
```

## Requirements

- .NET 10.0 SDK
