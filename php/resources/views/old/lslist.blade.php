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
Where T_Sponsors.SponsorType ='L' " );
?>
<table class="table table-bordered table-condensed">
<thead>
<tr><td colspan="6" align="center"><font size = "4" color="red"> GTDL League Sponsors and Partners</td></tr>
<tr valign="center">
  <th>Sponsor</th>
  <th>Address</th>
  <th>Phone</th>
  <th>Email</th>
  <th>Social Media</th>
  <th width="200px">What do they help us with?</th>
</tr>
</thead>
<tbody>
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


                    <td>  <?php echo $row['SponsorPhone']?></td>


       <?php
if ($row['SponsorEmail'] <> "" )
 { ?>		    <td>  <a href="mailto:<?php echo $row['SponsorEmail']?>"> Email Them! </a> </td> <?php }
 		Else { ?> <td>  No Email </td>
 <?php }
  ?>


 <?php
if ($row['SponsorFB'] !== "" )
 { ?>		    <td>  <a href= <?php echo $row['SponsorFB']?>  target="_blank"><img alt="" src="http://www.trentondarts.com/images/facebook.png" title="Facebook" width="32" /></a></td> <?php }
 		Else { ?> <td>  None </td>
 <?php }
  ?>


                    <td>  <?php echo $row['Description']?></td>



                  </tr>
                  <?php
}

  ?>
</tbody>
</table>
