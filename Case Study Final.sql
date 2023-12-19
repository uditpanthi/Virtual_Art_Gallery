use VirtualGallery

-- Create Artist table
CREATE TABLE Artist (
    ArtistID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100),
    Biography TEXT,
    BirthDate DATE,
    Nationality VARCHAR(50),
    Website VARCHAR(255),
    ContactInformation VARCHAR(255)
);

-- Create Artwork table
CREATE TABLE Artwork (
    ArtworkID INT PRIMARY KEY IDENTITY(1,1),
    Title VARCHAR(255),
    Description TEXT,
    CreationDate DATE,
    Medium VARCHAR(100),
    ImageURL VARCHAR(255),
    ArtistID INT,
    FOREIGN KEY (ArtistID) REFERENCES Artist(ArtistID) ON DELETE CASCADE
);

-- Create User table
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) unique,
    Password VARCHAR(50),
    Email VARCHAR(100),
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    DateOfBirth DATE,
    ProfilePicture VARCHAR(255)
);





-- Create FavoriteArtworks table (Many-to-Many relationship between User and Artwork)
CREATE TABLE FavoriteArtworks (
    UserID INT,
    ArtworkID INT,
    PRIMARY KEY (UserID, ArtworkID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID)ON DELETE CASCADE
);

-- Create Gallery table
CREATE TABLE Gallery (
    GalleryID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100),
    Description TEXT,
    Location VARCHAR(255),
    CuratorID INT,
    OpeningHours VARCHAR(255),
    FOREIGN KEY (CuratorID) REFERENCES Artist(ArtistID)ON DELETE CASCADE
);

-- Create Artwork_Gallery table (Many-to-Many relationship between Artwork and Gallery)
CREATE TABLE Artwork_Gallery (
    ArtworkID INT,
    GalleryID INT,
    PRIMARY KEY (ArtworkID, GalleryID),
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID)ON DELETE CASCADE,
    FOREIGN KEY (GalleryID) REFERENCES Gallery(GalleryID)ON DELETE CASCADE
);



-- Insert into Artist table
INSERT INTO Artist (Name, Biography, BirthDate, Nationality, Website, ContactInformation)
VALUES
( 'Vincent van Gogh', 'Dutch post-impressionist painter.', '1853-03-30', 'Dutch', 'https://www.vangoghgallery.com/', 'info@vangogh.com'),
( 'Leonardo da Vinci', 'Italian polymath of the Renaissance.', '1452-04-15', 'Italian', 'https://leonardodavinci.net/', 'info@leonardodavinci.net'),
( 'Pablo Picasso', 'Spanish painter and sculptor.', '1881-10-25', 'Spanish', 'https://www.picasso.fr/', 'info@picasso.fr'),
( 'Frida Kahlo', 'Mexican painter known for her self-portraits.', '1907-07-06', 'Mexican', 'https://www.fridakahlo.org/', 'info@fridakahlo.org'),
( 'Claude Monet', 'French impressionist painter.', '1840-11-14', 'French', 'https://www.claudemonetgallery.org/', 'info@claudemonetgallery.org'),
( 'Georgia O Keeffe', 'American modernist artist.', '1887-11-15', 'American', 'https://www.okeeffemuseum.org/', 'info@okeeffemuseum.org'),
( 'Salvador Dalí', 'Spanish surrealist painter.', '1904-05-11', 'Spanish', 'https://www.salvadordali.com/', 'info@salvadordali.com'),
( 'Rembrandt van Rijn', 'Dutch painter and etcher.', '1606-07-15', 'Dutch', 'https://www.rembrandtpainting.net/', 'info@rembrandtpainting.net'),
( 'Michelangelo Buonarroti', 'Italian sculptor, painter, and architect.', '1475-03-06', 'Italian', 'https://www.michelangelo.com/', 'info@michelangelo.com'),
( 'Edvard Munch', 'Norwegian painter known for "The Scream."', '1863-12-12', 'Norwegian', 'https://www.edvardmunch.org/', 'info@edvardmunch.org');

-- Insert into Artwork table
INSERT INTO Artwork ( Title, Description, CreationDate, Medium, ImageURL, ArtistID)
VALUES
( 'Starry Night', 'Oil painting depicting the night sky.', '1889-06-18', 'Oil on canvas', 'https://www.example.com/starry-night.jpg', 1),
( 'Mona Lisa', 'Famous portrait painting.', '1503-04-15', 'Oil on poplar panel', 'https://www.example.com/mona-lisa.jpg', 2),
( 'Guernica', 'Pablo Picasso powerful anti-war painting.', '1937-09-08', 'Oil on canvas', 'https://www.example.com/guernica.jpg', 3),
( 'The Two Fridas', 'Frida Kahlo iconic double self-portrait.', '1939-09-29', 'Oil on canvas', 'https://www.example.com/two-fridas.jpg', 4),
( 'Water Lilies', 'Claude Monet series of water lily paintings.', '1919-08-15', 'Oil on canvas', 'https://www.example.com/water-lilies.jpg', 5),
( 'Red Canna', 'Georgia O Keeffe vibrant flower painting.', '1924-01-01', 'Oil on canvas', 'https://www.example.com/red-canna.jpg', 6),
( 'The Persistence of Memory', 'Salvador Dalí iconic melting clocks.', '1931-06-28', 'Oil on canvas', 'https://www.example.com/persistence-of-memory.jpg', 7),
( 'The Night Watch', 'Rembrandt famous group portrait.', '1642-07-22', 'Oil on canvas', 'https://www.example.com/night-watch.jpg', 8),
( 'David', 'Michelangelo marble statue of David.', '1504-09-08', 'Marble', 'https://www.example.com/david.jpg', 9),
( 'The Scream', 'Edvard Munch iconic expressionist painting.', '1893-12-22', 'Oil on canvas', 'https://www.example.com/the-scream.jpg', 10);

-- Insert into Users table
INSERT INTO Users ( Username, Password, Email, FirstName, LastName, DateOfBirth, ProfilePicture)
VALUES
('uditpanthi','Upanthi@123','uditpanthi31@gmail.com','Udit','Panthi','2001-08-31','dp.png'),
( 'artlover123', 'password123', 'artlover@example.com', 'John', 'Doe', '1990-05-20', 'https://www.example.com/john-doe.jpg'),
( 'creativemind', 'pass123', 'creative@example.com', 'Jane', 'Smith', '1985-12-10', 'https://www.example.com/jane-smith.jpg'),
( 'galleryowner', 'gallerypass', 'owner@example.com', 'Gallery', 'Owner', '1970-03-15', 'https://www.example.com/gallery-owner.jpg'),
( 'curator', 'curatorpass', 'curator@example.com', 'Art', 'Curator', '1982-08-18', 'https://www.example.com/art-curator.jpg'),
( 'visitor1', 'visitorpass1', 'visitor1@example.com', 'Visitor', 'One', '1995-11-25', 'https://www.example.com/visitor1.jpg'),
( 'visitor2', 'visitorpass2', 'visitor2@example.com', 'Visitor', 'Two', '1988-07-12', 'https://www.example.com/visitor2.jpg'),
( 'adminuser', 'adminpass', 'admin@example.com', 'Admin', 'User', '1980-01-30', 'https://www.example.com/admin-user.jpg'),
( 'testuser1', 'testpass1', 'test1@example.com', 'Test', 'User1', '1993-09-05', 'https://www.example.com/test-user1.jpg'),
( 'testuser2', 'testpass2', 'test2@example.com', 'Test', 'User2', '1998-04-14', 'https://www.example.com/test-user2.jpg'),
( 'demo', 'demopass', 'demo@example.com', 'Demo', 'User', '2000-12-01', 'https://www.example.com/demo-user.jpg');

-- Insert into FavoriteArtworks table
INSERT INTO FavoriteArtworks (UserID, ArtworkID)
VALUES
(1, 1),
(1, 2),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9);


-- Insert into Gallery table
INSERT INTO Gallery ( Name, Description, Location, CuratorID, OpeningHours)
VALUES
( 'Art Haven', 'Contemporary art gallery', '123 Main Street, Cityville', 4, 'Mon-Fri: 9 AM - 6 PM, Sat: 10 AM - 4 PM'),
( 'Classic Collections', 'Classic art gallery', '456 Art Avenue, Townsville', 8, 'Tue-Sat: 10 AM - 5 PM'),
( 'Modern Art Space', 'Modern and abstract art gallery', '789 Art Road, Metropolis', 7, 'Wed-Sun: 11 AM - 7 PM'),
( 'Nature Canvas', 'Gallery focused on nature-themed artwork', '101 Green Lane, Countryside', 6, 'Thu-Sat: 12 PM - 8 PM'),
( 'Expressions Gallery', 'Showcasing diverse artistic expressions', '202 Art Street, Urbanville', 2, 'Fri-Sun: 1 PM - 9 PM'),
( 'Sculpture Haven', 'Gallery dedicated to sculptural art', '303 Sculptor Square, Sculpture City', 9, 'Sat-Sun: 10 AM - 6 PM'),
( 'Colorful Creations', 'Vibrant gallery with colorful artwork', '404 Color Avenue, Rainbow City', 5, 'Sun: 2 PM - 6 PM'),
( 'Historical Masterpieces', 'Showcasing historical and classical art', '505 History Lane, Pastopolis', 1, 'Mon-Thu: 10 AM - 4 PM'),
( 'Imagination Gallery', 'Explore the realm of imaginative artwork', '606 Dream Street, Fantasia', 3, 'Fri-Sat: 2 PM - 10 PM'),
( 'Digital Art Hub', 'Gallery focused on digital and new media art', '707 Tech Avenue, Digitalville', 10, 'Sat-Sun: 11 AM - 5 PM');

-- Insert into Artwork_Gallery table
INSERT INTO Artwork_Gallery (ArtworkID, GalleryID)
VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);










-- Update query

-- Update Artist's Website
UPDATE Artist
SET Website = 'http://www.updated-website.com'
WHERE ArtistID = 1;

-- Delete queries

-- Delete User
DELETE FROM Users
WHERE UserID = 1;

-- Delete Favorite Artwork
DELETE FROM FavoriteArtworks
WHERE UserID = 1 AND ArtworkID = 1;



SELECT * FROM Artwork


INSERT INTO Users ( Username, Password, Email, FirstName, LastName, DateOfBirth, ProfilePicture)
VALUES
('Vartlover123', 'password123', 'Vartlover@example.com', 'Johnny',
'Doye', '1990-05-20', 'https://www.example.com/johnny-doye.jpg')

UPDATE Artwork SET Title ='Starry Night II', 
Description = 'A sequel to the original Starry Night.', 
CreationDate = '1890-06-18', 
Medium = '', 
ImageURL = '',
ArtistID = 1 
WHERE ArtworkID =11


