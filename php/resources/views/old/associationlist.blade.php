<?php

$assoc= $associd;

$dbcnx = mysqli_connect("localhost", "trenton_webuser", "d@rts", "trenton_GTDL");

if (!$dbcnx) {
  echo( "<P>Unable to connect to the " .
        "database server at this time.</P>" );
  exit();
}

$leaguedetails= mysqli_query($dbcnx,
 "SELECT * from T_Associations
Where T_Associations.AssociationID=$assoc" );

	if (!$leaguedetails) {
  echo("<P>Error performing query: " .
       mysql_error() . "</P>");
  exit();
}

$row = mysqli_fetch_assoc($leaguedetails);
?>

<h1 align="center"><?php echo $row['AssocName']?></h1>
<table style="width: 90%; margin-right:auto; margin-left: auto;" class="table table-bordered table-condensed">
  <thead>
    <tr valign="center">
      <th scope="col"><p>Association Description</p></th>
      <th scope="col"><p>Association Map</p></th>
    </tr>
  </thead>
  <tr>
    <td><?php echo $row['Description']?></td>
    <td><a href= <?php echo $row['AssocMap']?> target="_blank"> Click for Map</td>
  </tr>
</table>

<?php

$leaguelist= mysqli_query($dbcnx,
 "SELECT LeagueName, LeagueAbbr, LeagueLocation, LeagueURL, LeagueFB, LeagueForum, LeagueContact, LeagueType, LeagueVenue, T_Leagues.LeagueID, T_LeagueDetails.LeagueID AS ID2, left(LeagueContact,4) AS LC4 FROM `T_Leagues`
Join (T_AssocMembers) on (T_Leagues.LeagueID=T_AssocMembers.LeagueID)
Join (T_Associations) on (T_AssocMembers.AssociationID = T_Associations.AssociationID)
LEFT JOIN (T_LeagueDetails) ON (T_Leagues.LeagueID = T_LeagueDetails.LeagueID)
Where T_Associations.AssociationID=$assoc
Order by LeagueName ASC" );

	if (!$leaguelist) {
  echo("<P>Error performing query: " .
       mysql_error() . "</P>");
  exit();
}
  ?>
<table style="width: 90%; margin-right:auto; margin-left: auto;" class="table table-bordered table-stripped table-condensed">
<thead>
<tr valign="center">
  <th scope="col"><p>League (Click for Details)</p></th>
  <th scope="col"><p>Location</p></th>
  <th scope="col"><p>Type</p></th>
  <th scope="col"><p>Venue</p></th>
  <th scope="col"><p>Website</p></th>
  <th scope="col"><p>Social Media</p></th>
  <th scope="col"><p>Contact</p></th>
</tr>
</thead>
<tbody>
<?php
while ( $row = mysqli_fetch_assoc($leaguelist) ) {
?>
<tr>
 <?php

 if($row['ID2'] === NULL)
 { ?><td>  <?php echo $row['LeagueName']?> </td>   <?php }
Else { ?> <td> <a href="{{ asset('old/leaguedetails') }}?lid=<?php echo $row['LeagueID']?>" > <?php echo $row['LeagueName'] ?> </td></td>
 <?php }
?>


                    <td>  <?php echo $row['LeagueLocation']?> </td>
                    <td>  <?php echo $row['LeagueType']?></td>
                    <td>  <?php echo $row['LeagueVenue']?></td>

   <?php
if ($row['LeagueURL'] !== "" )
 { ?>		    <td>  <a href= <?php echo $row['LeagueURL']?> target="_blank"> Website </td> <?php }
 		Else { ?> <td>   </td>
 <?php }
  ?>


 <?php
if ($row['LeagueFB'] !== "" )
 { ?>		    <td>  <a href= <?php echo $row['LeagueFB']?>  target="_blank"><img alt="" src="http://www.trentondarts.com/images/facebook.png" title="Facebook" width="40" height="40" /></a></td> <?php }
 		Else { ?> <td>  </td>
 <?php }
  ?>

   <?php
if ($row['LC4'] == "http" )
 { ?>		    <td>  <a href= <?php echo $row['LeagueContact']?> target="_blank"> Contact Form/Page</td> <?php }
 		Else { ?> <td>  <a href="mailto:<?php echo $row['LeagueContact']?>"> <?php echo $row['LeagueContact'] ?></a> </td>
 <?php }
  ?>
                  </tr>
                  <?php
}
  ?>
</tbody>
</table>
<div style="text-align: center">
Please Note---  This page is a work in progress!  If you have ideas on how to make the information to better connect dart players and leagues in NJ, mail the webmaster! <br/>
Want to be included in the Association listed above?  <a href="mailto:webmaster@trentondarts.com">Contact the GTDL webmaster</a>
</div>
