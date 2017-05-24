-- phpMyAdmin SQL Dump
-- version 4.3.8
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Jul 30, 2015 at 10:11 AM
-- Server version: 5.5.42-37.1
-- PHP Version: 5.4.23

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `trenton_DFD`
--

-- --------------------------------------------------------

--
-- Table structure for table `T_Events`
--

CREATE TABLE IF NOT EXISTS `T_Events` (
  `EventID` int(3) NOT NULL,
  `Year` int(4) DEFAULT NULL,
  `Date` date DEFAULT NULL,
  `EventName1` varchar(20) DEFAULT NULL,
  `EventName2` varchar(16) DEFAULT NULL,
  `Goal` int(5) DEFAULT NULL,
  `TotalDonation` int(5) DEFAULT NULL,
  `MAFNJTotal` int(5) DEFAULT NULL,
  `FinalTotal` int(5) NOT NULL,
  `Venue` varchar(50) DEFAULT NULL,
  `VenueLocation` varchar(20) DEFAULT NULL,
  `VenueCity` varchar(8) DEFAULT NULL,
  `VenueState` varchar(2) DEFAULT NULL,
  `VenueZip` varchar(5) DEFAULT NULL,
  `Attendees` varchar(3) DEFAULT NULL,
  `Story` text,
  `Darts` int(3) NOT NULL DEFAULT '51',
  `wlow` int(2) NOT NULL,
  `whigh` int(2) NOT NULL,
  `weather` varchar(100) NOT NULL
) ENGINE=MyISAM AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `T_Events`
--

INSERT INTO `T_Events` (`EventID`, `Year`, `Date`, `EventName1`, `EventName2`, `Goal`, `TotalDonation`, `MAFNJTotal`, `FinalTotal`, `Venue`, `VenueLocation`, `VenueCity`, `VenueState`, `VenueZip`, `Attendees`, `Story`, `Darts`, `wlow`, `whigh`, `weather`) VALUES
(1, 2006, '2006-05-13', 'Inaugural', 'Darts-for-Dreams', 5000, 5769, 5844, 5769, 'Conduit Music Club', 'Broad St', 'Trenton', 'NJ', '', '53', 'This year, we had three boards at the event.  They were setup around the stage at the club.  We had raffles and an auction table near a bar in the other corner.  It was dark.  We had two bands \nplaying (hoping that would draw in people).  It was loud and distracting.  </br>\n\nOur sponsor list the first year is almost exclusively three groups.  The local sports teams in New Jersey, the businesses that our players own or work at, and our league bars and sponsors.  \n\nThe exceptions were a donation by Conrad Daniels; yes, that Conrad Daniels who lives locally and owned the local Re/Max Realty office with his wife, several signed books by our long time friend \nGeorge Silberzhan, and a box of dart boards and darts from Bottlesen, who we connected with thanks to another long time friend, Jimmy Widmayer. </br></br>\n\nWe did a small ad-book using Microsoft Word.  I think it was 2 pages folder over, so 8 sides, listing the sponsors and addresses.  Very simple. </br></br>\n\nIt was beautiful spring day.  It was the day before Mother''s day.  We didn''t know how to communicate the event.  We really knew nothing.  But, we pulled it off, and raised $5800 for MAWNJ.  We decided to move forward.Trophies are donated by J & V Trophy.  They do our league trophies and he''s happy to step up for this event. ', 100, 50, 71, 'Mostly cloudy all day.  Some slight rain.'),
(2, 2007, '2007-03-24', '2nd Annual', 'Darts-for-Dreams', 7500, 8425, 8385, 8385, 'VFW 3525', '77 Christine Ave', 'Hamilton', 'NJ', '08610', '60', 'We moved venues this year.  The league had built additional stands, and we used all eight of them.  We set them up along one wall at VFW 3525, and had tables and chairs in the rest of the \nroom.  The from of the room had a small stage, so we set up the control desk there and all the prizes.  The bar is in a different room and the kitchen opens to the bar and the hall.  All the \nfood was $5, and $1 of each was given back to the event.  </br></br>\nThe sponsor list was pretty similar, with a few restaurants in the area that someone had the courage to bring a solicitation letter to.  We also had a local printer agree to donate books to \nthe event.  We created individual pages and converted them to PDFs, then sent them to him to print and bind.  For the first time, we did a T-shirt.  Not the best process but we got the shirts with all the logos on the back, squished to fit, but they fit!  </br></br>\n\nWe held some of the prizes until the end, and had a Luck of the Draw afterwards.  The entry fees \nwent into the donation, the winners got prizes.', 100, 44, 54, 'Overcast most of the day.  Rain after the event.'),
(3, 2008, '2008-04-12', '3rd Annual', 'Darts-for-Dreams', 10000, 11119, 12114, 12114, 'Arbeiter Hall', '151 East Franklin St', 'Trenton', 'NJ', '08619', '65', 'Due to a change in leadership at the VFW, and a desire by the then Commander to charge us to use the hall, we moved to Arbeiter Hall.  The local Ancient Order of Hibernians were using the \nbasement for their club meetings and had a relationship there.  So we moved.  Everything was setup, similar to the VFW (the halls are very similar in size/shape).  We had music again - this time Tom Glover.  A local musician who plays Irish music, Tom camped out on stage and played a few sets during the day as background music. </br></br>\n\nThen it got interesting.  We were scheduled to end at 7 PM.  At about 4 PM, Joe (I think that''s his name) tells Tony, a board member and our AOH contact with the hall that he has a party coming in and we need to get out.  Huh???  So we go back and forth a little and decide to drag the boards into a separate bar area, and we''d deal with them there.  So we bring the boards, all the prizes, items, administrative paperwork, and 70 people into a bar area that''s about 400, maybe \n500 square feet.  The hall for comparison is about 50 by 80 feet, maybe 4000 square feet?  We finish up in the bar, giving out prizes, doing the raffles and the silent auction all while the hall fills up with the next party.  Never again!!!  After we clean up, we head downstairs into the AOH club area, and Joe''s drinking and buying us drinks.  Talk about a two-headed personality. </br></br>\n\nWe move the shirt printing over to Crown Trophy, owned by a league member.  We start to print different colors for the Event Staff so people can find us during the event.', 100, 55, 75, 'Scattered clouds throughout the day.  Cleared up later.'),
(4, 2009, '2009-03-07', '4th Annual', 'Darts-for-Dreams', 12000, 18212, 19512, 19152, 'VFW 3525', '77 Christine Ave', 'Hamilton', 'NJ', '08610', '70', 'For 2009, we go back to VFW 3525. We  get a nice check from the Wal-mart Foundation for $2,000.   What a surprise that was to get the call to stop by.  I thought I was going to pick up a gift card or two (that finally made it through to the right person).  I get over to the store and they hand me the check!  Wow!!! </br></br>\n\nThis is also the first time we sell raffles prior to the event. We do 1000 tickets at $5 each, and the prizes were a 50" TV and a digital camera.  The raffle alone increased our donation to MAWNJ by over $4,000 and is the primary reason for the spike in our totals.  Maybe we''re on to something here. </br></br>\n\nWe move the books to another printer this year, who donates their services.  They give me a \ntemplate to drop the ads into, and they run the job.  Works great.  Unfortunately, it''s the only \nyear they provide the service.  Luckily the templates will be useful in the future.', 100, 44, 72, 'clear all day.  Nothing in the sky at all.'),
(5, 2010, '2010-04-10', '5th Annual', 'Darts-for-Dreams', 17000, 21102, 21035, 21035, 'VFW 3525', '77 Christine Ave', 'Hamilton', 'NJ', '08610', '89', 'Another year at VFW 3525.  For the first time, we raise over $20,000!   We also have 100 or more sponsors for the first time.  Many of the new additions to our sponsorship are for raffles or \nauction items.  Our financial donors stay about the same.  </br></br>\n\nThe 2010 event is also the first where we officially have a youth event.  All of the youth in attendance get a small trophy for coming out, and they get to pick their prize from the raffle \nbin before we actually pull the raffles.  Most choose sports tickets to the various minor league teams in the area (Trenton Titans/Devils and Thunder, Somerset Patriots, Camden Riversharks.)</br></br>\n\n2010 is also our 5th year, and we recognize seven sponsors for being on board each of our first five events. They were:  Camden Riversharks, Firkin Tavern, J and V Trophies, Marty’s Place, NJ Devils, Ritchie and Page Distributors and the Trenton Thunder.  We''ll continue to recognize the 5 \nyear sponsors each year going forward with a plaque recognizing their support.\n</br></br>\nWe sell TV raffles again, this time almost selling out 1200 tickets.  Another huge win for the event and MAWNJ.  We also continue the ad-book ourselves.  The templates work from last year work \nfor us, and we find a local printer that will charge us cost for the printing.  A huge savings over the retail cost for printing and layout.', 51, 44, 63, 'Rain in the morning.  Mostly cloudy throughout the day.'),
(6, 2011, '2011-04-11', '6th Annual', 'Darts-for-Dreams', 17000, 17102, 18703, 18703, 'Cook Athletic Association', '411 Hobart Ave', 'Hamilton', 'NJ', '08629', '71', 'In 2011 we move again to another hall.  This time it''s Cook Athletic Association, or Cook AA or \njust ''The Cook'' as it''s known.  It''s a neighborhood social club that has a full gym in the back.  The club has been around for 50 years providing neighborhood sports clubs a place to use.  It''s on a residential street and you''d drive by it 100 times before realizing it''s there.  Like the \nVFW in the past, it has parking, a bar, a kitchen, and enough space to spread out 8 dart stands.  \n</br></br>\n We set the room up similarly, with a welcome station to check in, the prizes and control desk across the room, the dartboards on the right wall, kitchen and bathrooms to the left, plus tables \n in the middle.  The bar is a separate room out front. \n</br></br>\nWe again sell raffle tickets for a TV, iPad and an iPod.  We sell 1125 before the event and some \nat the event.  We printed 1500 this year.  The big difference in 2011 is that the licenses don''t \nget back in time to print and take tickets to the Rae Chesney/PA Open event.  I''ve sold up to 200 \n there in the past and it''s a great start.  Unfortunately, not this year.  \n</br></br>\nIn 2011, we''re also part of an Internet radio show called Darts around the World.  Ted Northrop connects us with Dave, Bones and the crew and we''re interviewed down at Squire McGuire''s in \nPhilly about the event.  \n</br></br>\nThe events seem to be going smoother.  Repeat attendees know the deal.  New ones catch on quick.  The volunteers are really there to help move things along.  People seem happy to be out, having fun and raising money.\n\n</br></br>\nWe get our first international sponsor.  We connect with Nozomi over at Cosmo Darts who sends a \nsmall box of items to use. This will turn out to be a great relationship over the years as she continues to support the event going forward', 51, 49, 86, 'Cloudy in the morning.  Clear all day otherwise.'),
(7, 2012, '2012-04-28', '7th Annual', 'Darts-for-Dreams', 20000, 20035, 21290, 21290, 'Cook Athletic Association', '411 Hobart Ave', 'Hamilton', 'NJ', '08629', '64', 'We''re at Cook AA again this year, and we''re back over the $20,000 mark.  Our raffles are back on track, selling around 1400 of the 1500 this year.  We update the prizes a little, and besides a \nTV, we do an iPad again, plus an XBox 360 package.  The prizes seem to be a hit.  Even with the increase in the prize costs, the raffles bring in over $5,000 in profit!  \n\n</br></br>\n\nIn 2012, we go over the 40 mark for financial sponsorships.  That makes a big difference in the totals.  ', 51, 33, 59, 'Clear all day.  Clouds moved in at the end of the event.'),
(8, 2013, '2013-04-13', '8th Annual', 'Darts for Dreams', 20000, 28138, 28138, 28338, 'Cook Athletic Association', '411 Hobart Ave', 'Hamilton', 'NJ', '08629', '103', 'This is our third year at Cook AA.  Our sponsor count is similar to the past, but when all is said and done, our total is up by 30%!!!  We are astonished at the final amount and still today, can''t figure out why.  We did have over 100 attendees for the first time.  If we count every \nattendee as say, $50, there''s possibly another $2,000 there.  Our auction items are up a little, and some of them bring in big dollars. Foley''s Shop n Bag comes on board as a new sponsor, and platinum at that.  Everything helps!!!  \n</br></br>\nWe increase the number of raffle tickets for sale again this year.  We go to 2000.  It puts less pressure on everyone and leaves plenty of tickets for people attending the event to buy some.  We sell over 1600 of them this year, and with the prizes the same as last year, profit about $6,000. \n</br></br>\n \n2013 is the first year we use an online payment option.  While I don''t think it generates any additional donations, it makes it easy for many people and removes a pile of checks from the mix.', 51, 38, 59, 'Cloudy throughout the day. No rain however.'),
(9, 2014, '2014-05-03', '9th Annual', 'Darts for Dreams', 25000, 28172, 28172, 28372, 'Cook Athletic Association', '411 Hobart Ave', 'Hamilton', 'NJ', '08629', '81', 'Well, we''re firmly settled in at Cook for the 4th year.  We setup Friday night as always, and this year we''re done in about 3 1/2 hours.  Everyone seems to know what''s going on.  The Cook \nvolunteers have tables out when we get there.  The piles of prizes and equipment are unloaded from the office to their respective homes pretty quickly.  The gift bags are stuffed and piled for Saturday.  The Cook members keep popping in to take a look and see what they can win on Saturday. </br></br>\n\nIt turns out the choice of day isn''t the best.  We''re up against some Communions for some of our players, and the usually robust Philly women''s crowd heads to Maryland for "It''s a Woman''s Thing", which raises money for the Susan Komen Foundation.  It''s their 20th year, and the Women head down there.  I knew that would happen, and wish them the best.  It''s one of the downsides of \nEast Coast Darts - there is something every weekend, sometimes more.  I used to worry that I was scheduling against the popular Virgina Beach Tournament, so I''d move it- but the reality is the \nplayers in the GTDL don''t travel much, so it''s not a big deal.  Now, I work with the other leagues in NJ and Philly to make sure there''s no conflicts and go from there. </br></br>\n\nA nice benefit this year was a donation made by the players at a small North NJ event called the Kentucky Derby of Darts.  They ran a 50/50 and sent us the proceeds.  $340 worth!  Another bonus \nthis year; the winning ticket for the iPad said "Give it Back!".  So, we sold $5 raffles at the event right there and collected over $400 more for the iPad. </br></br>\n\nCook decided to host a Karaoke party in the lounge after darts.  It''s packed!  Must be 40-50 people in until midnight or so.  Everyone as a good time.  I''m guessing it''s a tradition they''ll \nkeep up with as well.\n\nIn 2014 we really start to leverage Facebook.  It generates interest in the darts community and we net about 6 additional darts related sponsors to the cause.  We''ll have to continue to use social media in the future and hopefully leverage that network to continue to grow.  \n', 51, 46, 69, 'Clear in the morning.  Clouds moved in for the afternoon.'),
(10, 2015, '2015-04-25', '10th Annual', 'Darts for Dreams', 25000, 32262, NULL, 32262, 'Cook Athletic Association', '411 Hobart Ave', 'Hamilton', 'NJ', '08629', '86', 'So, we''re only in August, but we''re already starting to get things done.  I''ve filed paperwork with MAWNJ for the event.  I''ve confirmed with Cook AA the dates.  I expect to have raffle applications in by mid-September.  It seems like each year, there''s always some holdup on raffles, so the earlier the better.  Let''s hope this year. <br>  January 2nd...  Raffles are in hand - I''ll start selling at the Chesney next weekend.   Also, I expect the letters to the local sports teams to go out this week.  Those items are always a big hit and really drive the silent auction.  I''ll also likely get the dart companies started pretty early.  In general they''ve been great and also send quite a bit of product that we use in a variety of ways.   <br><br>  So we''re a few days out now.   Shirts and the book are out for printing.   We have a total of 170 sponsors this year.  That''s up a bunch from last year.   <br><br> It also looks like we''re going to crush the $25,000 goal, and with some luck, we could hit $30,000!!! <br><br>So we came in at around $32,500 initially.  There was some talk that the ABCD and Tri-County leagues in North Jersey were going to try and send some donations.   Hope to see them soon.  We''ll be doing our check ceremony on April 30th, the same night as our GTDL A Division Finals.   <br><br>We also found out that this is the last year we''ll be working with Kristina Maglietta at MAWNJ.  She''s been our MAW contact for six years now, and has been instrumental in our success.  Kristina is moving on to Apple in a few weeks.  Good Luck Kristina!\n \n\n', 51, 33, 58, 'Clear all day!  Not a cloud in the sky.  A little brisk.');

-- --------------------------------------------------------

--
-- Table structure for table `T_Links`
--

CREATE TABLE IF NOT EXISTS `T_Links` (
  `DocumentID` int(3) NOT NULL,
  `EventID` int(3) NOT NULL,
  `DocName` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `DocType` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `DocURL` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `DocCategory` varchar(25) COLLATE utf8_unicode_ci NOT NULL,
  `Active` int(1) NOT NULL DEFAULT '1'
) ENGINE=MyISAM AUTO_INCREMENT=35 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `T_Links`
--

INSERT INTO `T_Links` (`DocumentID`, `EventID`, `DocName`, `DocType`, `DocURL`, `DocCategory`, `Active`) VALUES
(1, 2, 'Wish Kids Letter', 'pdf', 'http://www.trentondarts.com/events/2006-2007/mawty07.pdf', '', 1),
(2, 2, 'Ad-book', 'pdf', 'http://www.trentondarts.com/events/2006-2007/adbook07.pdf', '', 1),
(3, 2, 'Event T-shirt', 'pdf', 'http://www.trentondarts.com/events/2006-2007/shirt07final.pdf', '', 1),
(4, 3, 'Kids Wish Letter', 'pdf', 'http://www.trentondarts.com/events/2007-2008/mawty08.pdf', '', 1),
(5, 3, 'Ad Book', 'pdf', 'http://www.trentondarts.com/events/2007-2008/adbook08.pdf', '', 1),
(6, 3, 'T Shirt', 'pdf', 'http://www.trentondarts.com/events/2007-2008/shirt08final.pdf', '', 1),
(7, 4, 'Ad Book', 'pdf', 'http://www.trentondarts.com/events/2008-2009/adbook09.pdf', '', 1),
(8, 4, 'T Shirt', 'pdf', 'http://www.trentondarts.com/events/2008-2009/shirt09final.pdf', '', 1),
(9, 4, 'Pictures from Event', 'html', 'http://www.trentondarts.com/events/2008-2009/dfd4pics/index.html', '', 1),
(10, 5, 'Kids Wish Letter', 'pdf', 'http://www.trentondarts.com/events/2009-2010/mawty10.pdf', '', 1),
(11, 5, 'Ad Book', 'pdf', 'http://www.trentondarts.com/events/2009-2010/adbook10.pdf', '', 1),
(12, 5, 'T Shirt', 'pdf', 'http://www.trentondarts.com/events/2009-2010/shirt10final.pdf', '', 1),
(13, 6, 'Ad book', 'pdf', 'http://www.trentondarts.com/events/2010-2011/adbook11.pdf', '', 1),
(14, 6, 'T Shirt', 'pdf', 'http://www.trentondarts.com/events/2010-2011/shirt11final.pdf', '', 1),
(15, 7, 'Kids Wish Letter', 'pdf', 'http://www.trentondarts.com/events/2011-2012/mawty12.pdf', '', 1),
(16, 7, 'Ad Book', 'pdf', 'http://www.trentondarts.com/events/2011-2012/adbook12.pdf', '', 1),
(17, 7, 'Pictures', 'html', 'http://www.trentondarts.com/events/2011-2012/dfd7pics/index.html', '', 1),
(18, 8, 'Kids Wish Letter', 'pdf', 'http://www.trentondarts.com/events/2012-2013/mawty13.pdf', '', 1),
(19, 8, 'Ad Book', 'pdf', 'http://www.trentondarts.com/events/2012-2013/adbook13.pdf', '', 1),
(20, 8, 'T Shirt', 'jpg', 'http://www.trentondarts.com/events/2012-2013/shirt13final.jpg', '', 1),
(21, 8, 'Pictures', 'html', 'http://www.trentondarts.com/events/2012-2013/dfd8pics/index.html', '', 1),
(22, 9, 'Kids Wish Letter', 'pdf', 'http://www.trentondarts.com/events/2013-2014/mawty14.pdf', '', 0),
(23, 9, 'Ad Book', 'pdf', 'http://www.trentondarts.com/events/2013-2014/adbook14.pdf', '', 1),
(24, 9, 'T Shirt', 'img', 'http://www.trentondarts.com/events/2013-2014/shirt14final.jpg', '', 1),
(25, 7, 'T Shirt', 'jpg', 'http://www.trentondarts.com/events/2011-2012/shirt12final.jpg', '', 1),
(27, 9, 'Thank You Letter', 'jpg', 'http://www.trentondarts.com/events/2013-2014/tyletters14.jpg', '', 1),
(28, 9, 'Final donation report', 'pdf', 'http://www.trentondarts.com/events/2013-2014/donationreport.pdf', 'Stats', 1),
(29, 8, 'Final donation report', 'pdf', 'http://www.trentondarts.com/events/2012-2013/donationreport.pdf', 'Stats', 1),
(30, 7, 'Final donation report', 'pdf', 'http://www.trentondarts.com/events/2011-2012/donationreport.pdf', 'Stats', 1),
(31, 9, 'Wish Kid''s letters', 'pdf', 'http://www.trentondarts.com/events/2013-2014/wishletter14.pdf', '', 1),
(32, 10, 'Online Donation Form', '', 'http://friends.wish.org/003-000/page/Greater-Trenton-Dart-League/Darts-for-Dreams-10.htm', '', 1),
(33, 10, 'T Shirt', 'jpeg', 'http://www.trentondarts.com/events/2014-2015/tshirt2015.jpg', '', 1),
(34, 10, 'Ad Book', 'pdf', 'http://www.trentondarts.com/events/2014-2015/adbook15.pdf', '', 1);

-- --------------------------------------------------------

--
-- Table structure for table `T_Winners`
--

CREATE TABLE IF NOT EXISTS `T_Winners` (
  `WinnerID` int(3) NOT NULL,
  `EventID` int(3) DEFAULT NULL,
  `Category` varchar(7) DEFAULT NULL,
  `Name` varchar(21) DEFAULT NULL,
  `Total` int(5) DEFAULT NULL
) ENGINE=MyISAM AUTO_INCREMENT=38 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `T_Winners`
--

INSERT INTO `T_Winners` (`WinnerID`, `EventID`, `Category`, `Name`, `Total`) VALUES
(1, 1, 'Male', 'Jim Widmayer', 3220),
(2, 1, 'Female', 'Mary Jo Chesney', 2084),
(3, 1, 'Team', 'USA Darts', 9572),
(4, 2, 'Male', 'Rob Bodolosky', 2954),
(5, 2, 'Female', 'Terri Marcello', 2231),
(6, 2, 'Team', 'Gators', 8381),
(7, 3, 'Male', 'Gary Autz', 2756),
(8, 3, 'Female', 'Terri Marcello', 2200),
(9, 3, 'Team', 'VFW Four Amigos', 8602),
(10, 4, 'Male', 'Paul Seladones', 2780),
(11, 4, 'Female', 'Sarah Lanternman', 1574),
(12, 4, 'Team', 'QCEDL', 10357),
(13, 5, 'Male', 'Paul Seladones', 1438),
(14, 5, 'Female', 'Christina Williams', 971),
(15, 5, 'Team', 'QCEDL', 5390),
(16, 6, 'Male', 'Jim Newman', 1723),
(17, 6, 'Female', 'Michelle Dolan', 977),
(18, 6, 'Team', 'QCEDL', 5601),
(19, 6, 'Youth', 'Michael Dougherty, Jr', 756),
(20, 7, 'Male', 'Russ Jasinski', 1614),
(21, 7, 'Female', 'Maria Ingram', 729),
(22, 7, 'Team', 'QCEDL', 4495),
(23, 7, 'Youth', 'Michael Dougherty, Jr', 832),
(24, 8, 'Male', 'Carl Flaherty', 1599),
(25, 8, 'Female', 'Theresa Sieger', 1069),
(26, 8, 'Team', 'Keith''s Crusaders', 5022),
(27, 8, 'Youth', 'Michael Dougherty, Jr', 802),
(28, 9, 'Male', 'Jim Newman', 1659),
(29, 9, 'Female', 'Mylinda Mannion', 1165),
(30, 9, 'Team', 'Philly ASL', 5718),
(31, 9, 'Youth', 'Michael Dougherty, Jr', 906),
(32, 10, 'Male', 'Timmy O''Brien', 1675),
(33, 10, 'Female', 'Kelly Gallagher', 965),
(34, 10, 'Team', 'Mr. Philly + 3', 4554),
(35, 10, 'Youth', 'Adrienne', 850);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `T_Events`
--
ALTER TABLE `T_Events`
  ADD PRIMARY KEY (`EventID`);

--
-- Indexes for table `T_Links`
--
ALTER TABLE `T_Links`
  ADD PRIMARY KEY (`DocumentID`);

--
-- Indexes for table `T_Winners`
--
ALTER TABLE `T_Winners`
  ADD PRIMARY KEY (`WinnerID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `T_Events`
--
ALTER TABLE `T_Events`
  MODIFY `EventID` int(3) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=11;
--
-- AUTO_INCREMENT for table `T_Links`
--
ALTER TABLE `T_Links`
  MODIFY `DocumentID` int(3) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=35;
--
-- AUTO_INCREMENT for table `T_Winners`
--
ALTER TABLE `T_Winners`
  MODIFY `WinnerID` int(3) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=38;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
