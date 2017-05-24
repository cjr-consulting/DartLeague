<?php
$dbcnx = mysqli_connect("localhost", "trenton_webuser", "d@rts", "trenton_GTDL");

if (!$dbcnx) {
  echo( "<P>Unable to connect to the " .
        "database server at this time.</P>" );
  exit();
}

$sponsorlist= mysqli_query($dbcnx,
 "SELECT SponsorName, concat(SponsorAddress,'<br> ',SponsorCity,' ',SponsorState,' ',SponsorZip) AS FullAddress ,SponsorPhone, SponsorURL, SponsorFB,SponsorMap,  SponsorEmail, Description
FROM T_Sponsors
Where T_Sponsors.SponsorType ='P' " );

	if (!$sponsorlist) {
  echo("<P>Error performing query: " .
       mysql_error() . "</P>");
  exit();
}
?>

<table class="table table-bordered table-condensed">
<tr><td colspan="6" align="center"><font size = "4" color="red"> GTDL Player Businesses </td></tr>
<tbody>
<tr valign="center">
  <th scope="col" width="25%"><p>Sponsor</p></th>
  <th scope="col" width="25%"><p>Address</p></th>
  <th scope="col" width="10%"><p>Phone</p></th>
  <th scope="col" width="5%"><p>Email</p></th>
  <th scope="col" width="5%"><p>Social Media</p></th>
  <th scope="col" width="30%"><p>Product or Service</p></th>
</tr>
</tbody>
<?php
while ( $row = mysqli_fetch_assoc($sponsorlist) ) {
?>
 		<tr>
   <?php
if ($row['SponsorURL'] <> "" )
 { ?>		    <td>  <a href= <?php echo $row['SponsorURL']?> target="_blank"> <?php echo $row['SponsorName'] ?> </td> <?php }
 		Else { ?> <td>  <?php echo $row['SponsorName'] ?></td>
 <?php }
  ?>

    <?php
if ($row['SponsorMap'] <> "" )
 { ?>		    <td>  <a href= <?php echo $row['SponsorMap']?> target="_blank"> <?php echo $row['FullAddress'] ?> </td> <?php }
 		Else { ?> <td>  <?php echo $row['FullAddress'] ?></td>
 <?php }
  ?>
  <td><?php echo $row['SponsorPhone']?></td>
  <?php
if ($row['SponsorEmail'] <> "" )
 { ?>
   <td><a href="mailto:<?php echo $row['SponsorEmail']?>"> Email Them! </a> </td> <?php }
 		Else { ?> <td>  No Email </td>
 <?php }
if ($row['SponsorFB'] !== "" )
 { ?>		    <td>  <a href= <?php echo $row['SponsorFB']?>  target="_blank"><img alt="" src="http://www.trentondarts.com/images/facebook.png" title="Facebook" width="40" height="40" /></a></td> <?php }
 		Else { ?> <td>  None </td>
 <?php }
  ?>
  <td>  <?php echo $row['Description']?></td>
</tr>
<?php
}
?>
</table>
