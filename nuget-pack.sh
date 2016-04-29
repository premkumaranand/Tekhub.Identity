packProject()
{
echo "****************************************"
echo "Packing $1"
echo "****************************************"
echo "                                        "
cd $1
rm -f $1*.nupkg
nuget pack -IncludeReferencedProjects
cp $1*.nupkg "K:\Software Development\Tekhub Nuget Packages"
cd ..
echo "                                        "
}


packProject Tekhub.Identity
packProject Tekhub.Identity.Web