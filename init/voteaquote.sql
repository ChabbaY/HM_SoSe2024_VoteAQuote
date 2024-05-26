-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: voteaquote_database
-- Erstellungszeit: 26. Mai 2024 um 15:08
-- Server-Version: 8.0.33
-- PHP-Version: 8.1.20

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `voteaquote`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Authors`
--

CREATE TABLE `Authors` (
  `Id` int NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `GivenName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Lifetime` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `Authors`
--

INSERT INTO `Authors` (`Id`, `Name`, `GivenName`, `Lifetime`, `Description`) VALUES
(1, 'Aristoteles', '', '(384 v.Chr. - 322 v.Chr.)', 'griechischer Philosoph'),
(2, 'Curie', 'Marie', '(1867-1934)', 'polnisch-französische Chemikerin und Physikerin'),
(3, 'Platon', '', '(ca. 427 - 347 v.Chr.)', 'griechischer Philosoph');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Quotes`
--

CREATE TABLE `Quotes` (
  `Id` int NOT NULL,
  `AuthorId` int NOT NULL,
  `Content` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `Quotes`
--

INSERT INTO `Quotes` (`Id`, `AuthorId`, `Content`) VALUES
(1, 1, 'Alle Menschen streben von Natur aus nach Wissen.'),
(2, 1, 'Es zeichnet einen gebildeten Geist aus, sich mit jenem Grad an Genauigkeit zufrieden zugeben, den die Natur der Dinge zulässt, und nicht dort Exaktheit zu suchen, wo nur Annäherung möglich ist.'),
(3, 1, 'So ist der Wucher hassenswert, weil er aus dem Geld selbst den Erwerb zieht und nicht aus dem, wofür das Geld da ist. Denn das Geld ist um des Tausches willen erfunden worden, durch den Zins vermehrt es sich dagegen durch sich selbst. […] Diese Art des Gelderwerbs ist also am meisten gegen die Natur.'),
(4, 1, 'Jede Bewegung verläuft in der Zeit und hat ein Ziel.'),
(5, 2, 'Ich gehöre zu denen, die die besondere Schönheit des wissenschaftlichen Forschens erfasst haben. Ein Gelehrter in einem Laboratorium ist nicht nur ein Techniker, er steht auch vor den Naturvorgängen wie ein Kind vor einer Märchenwelt.'),
(6, 2, 'Wir dürfen nicht hoffen, eine bessere Welt zu erbauen, ehe nicht die Individuen besser werden. In diesem Sinn soll jeder von uns an seiner eigenen Vervollkommnung arbeiten, indem er auf sich nimmt, was ihm im Lebensganzen der Menschheit an Verantwortlichkeit zukommt, und sich seiner Pflicht bewusst bleibt, denen zu helfen, denen er am ehesten nützlich sein kann.'),
(7, 3, 'Der Körper ist das Grab der Seele.'),
(8, 3, 'Den Guten nenne ich glücklich. Wer aber Unrecht tut, den nenne ich unglücklich.');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Votes`
--

CREATE TABLE `Votes` (
  `Id` int NOT NULL,
  `QuoteId` int NOT NULL,
  `User` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `VoteValue` int NOT NULL,
  `Timestamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `Votes`
--

INSERT INTO `Votes` (`Id`, `QuoteId`, `User`, `VoteValue`, `Timestamp`) VALUES
(9, 1, 'a', 1, '2024-05-06'),
(10, 1, 'b', 1, '2024-05-06'),
(11, 1, 'c', 1, '2024-05-06'),
(12, 2, 'a', 1, '2024-05-06'),
(13, 2, 'b', 1, '2024-05-06'),
(14, 2, 'c', 1, '2024-05-06'),
(15, 2, 'd', 1, '2024-05-06'),
(16, 2, 'e', 1, '2024-05-06'),
(17, 2, 'f', 1, '2024-05-06'),
(18, 2, 'g', 1, '2024-05-06'),
(19, 2, 'h', 1, '2024-05-06'),
(20, 3, 'a', 1, '2024-05-06'),
(21, 3, 'b', 1, '2024-05-06'),
(22, 3, 'c', 1, '2024-05-06'),
(23, 3, 'd', 1, '2024-05-06'),
(24, 3, 'e', 1, '2024-05-06'),
(25, 4, 'a', 1, '2024-05-06'),
(26, 5, 'a', 1, '2024-05-06'),
(27, 5, 'b', 1, '2024-05-06'),
(28, 6, 'a', 1, '2024-05-06'),
(29, 6, 'b', 1, '2024-05-06'),
(30, 6, 'c', 1, '2024-05-06'),
(31, 7, 'a', 1, '2024-05-06'),
(32, 7, 'b', 1, '2024-05-06'),
(33, 7, 'c', 1, '2024-05-06'),
(34, 8, 'a', 1, '2024-05-06'),
(35, 8, 'b', 1, '2024-05-06'),
(36, 8, 'c', 1, '2024-05-06'),
(37, 8, 'd', 1, '2024-05-06'),
(38, 8, 'e', 1, '2024-05-06');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `__EFMigrationsHistory`
--

CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `__EFMigrationsHistory`
--

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
('20240513144130_migration_v0', '8.0.2');

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `Authors`
--
ALTER TABLE `Authors`
  ADD PRIMARY KEY (`Id`);

--
-- Indizes für die Tabelle `Quotes`
--
ALTER TABLE `Quotes`
  ADD PRIMARY KEY (`Id`);

--
-- Indizes für die Tabelle `Votes`
--
ALTER TABLE `Votes`
  ADD PRIMARY KEY (`Id`);

--
-- Indizes für die Tabelle `__EFMigrationsHistory`
--
ALTER TABLE `__EFMigrationsHistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `Authors`
--
ALTER TABLE `Authors`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT für Tabelle `Quotes`
--
ALTER TABLE `Quotes`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT für Tabelle `Votes`
--
ALTER TABLE `Votes`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=39;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
