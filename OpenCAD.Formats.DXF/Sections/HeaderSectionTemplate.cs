using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace OpenCAD.Formats.DXF.Sections
{
    public partial class HeaderSection {
	/// <summary>
    /// Maintenance version number (should be ignored)
    /// </summary>
	[DXFObject("$ACADMAINTVER", 70)]
	public System.Int16 ACADMAINTVER {get; private set;}
	/// <summary>
    /// The AutoCAD drawing database version number:
    /// AC1006 = R10; AC1009 = R11 and R12;
    /// AC1012 = R13; AC1014 = R14; AC1015 = AutoCAD 2000;
    /// AC1018 = AutoCAD 2004
    /// </summary>
	[DXFObject("$ACADVER", 1)]
	public System.String ACADVER {get; private set;}
	/// <summary>
    /// Angle 0 direction
    /// </summary>
	[DXFObject("$ANGBASE", 50)]
	public System.Double ANGBASE {get; private set;}
	/// <summary>
    /// 1 = Clockwise angles
    /// 0 = Counterclockwise angles
    /// </summary>
	[DXFObject("$ANGDIR", 70)]
	public System.Int16 ANGDIR {get; private set;}
	/// <summary>
    /// Attribute visibility:
    /// 0 = None
    /// 1 = Normal
    /// 2 = All
    /// </summary>
	[DXFObject("$ATTMODE", 70)]
	public System.Int16 ATTMODE {get; private set;}
	/// <summary>
    /// Units format for angles
    /// </summary>
	[DXFObject("$AUNITS", 70)]
	public System.Int16 AUNITS {get; private set;}
	/// <summary>
    /// Units precision for angles
    /// </summary>
	[DXFObject("$AUPREC", 70)]
	public System.Int16 AUPREC {get; private set;}
	/// <summary>
    /// Current entity color number:
    /// 0 = BYBLOCK; 256 = BYLAYER
    /// </summary>
	[DXFObject("$CECOLOR", 62)]
	public System.Int16 CECOLOR {get; private set;}
	/// <summary>
    /// Current entity linetype scale
    /// </summary>
	[DXFObject("$CELTSCALE", 40)]
	public System.Double CELTSCALE {get; private set;}
	/// <summary>
    /// Entity linetype name, or BYBLOCK or BYLAYER
    /// </summary>
	[DXFObject("$CELTYPE", 6)]
	public System.String CELTYPE {get; private set;}
	/// <summary>
    /// Lineweight of new objects
    /// </summary>
	[DXFObject("$CELWEIGHT", 370)]
	public System.IntPtr CELWEIGHT {get; private set;}
	/// <summary>
    /// Plotstyle handle of new objects; if CEPSNTYPE is 3, then
    /// this value indicates the handle
    /// </summary>
	[DXFObject("$CEPSNID", 390)]
	public System.IntPtr CEPSNID {get; private set;}
	/// <summary>
    /// Plot style type of new objects:
    /// 0 = Plot style by layer
    /// 1 = Plot style by block
    /// 2 = Plot style by dictionary default
    /// 3 = Plot style by object ID/handle
    /// </summary>
	[DXFObject("$CEPSNTYPE", 380)]
	public System.IntPtr CEPSNTYPE {get; private set;}
	/// <summary>
    /// First chamfer distance
    /// </summary>
	[DXFObject("$CHAMFERA", 40)]
	public System.Double CHAMFERA {get; private set;}
	/// <summary>
    /// Second chamfer distance
    /// </summary>
	[DXFObject("$CHAMFERB", 40)]
	public System.Double CHAMFERB {get; private set;}
	/// <summary>
    /// Chamfer length
    /// </summary>
	[DXFObject("$CHAMFERC", 40)]
	public System.Double CHAMFERC {get; private set;}
	/// <summary>
    /// Chamfer angle
    /// </summary>
	[DXFObject("$CHAMFERD", 40)]
	public System.Double CHAMFERD {get; private set;}
	/// <summary>
    /// Current layer name
    /// </summary>
	[DXFObject("$CLAYER", 8)]
	public System.String CLAYER {get; private set;}
	/// <summary>
    /// Current multiline justification:
    /// 0 = Top; 1 = Middle; 2 = Bottom
    /// </summary>
	[DXFObject("$CMLJUST", 70)]
	public System.Int16 CMLJUST {get; private set;}
	/// <summary>
    /// Current multiline scale
    /// </summary>
	[DXFObject("$CMLSCALE", 40)]
	public System.Double CMLSCALE {get; private set;}
	/// <summary>
    /// Current multiline style name
    /// </summary>
	[DXFObject("$CMLSTYLE", 2)]
	public System.String CMLSTYLE {get; private set;}
	/// <summary>
    /// Shadow mode for a 3D object:
    /// 0 = Casts and receives shadows
    /// 1 = Casts shadows
    /// 2 = Receives shadows
    /// 3 = Ignores shadows
    /// </summary>
	[DXFObject("$CSHADOW", 280)]
	public System.IntPtr CSHADOW {get; private set;}
	/// <summary>
    /// Number of precision places displayed in angular dimensions
    /// </summary>
	[DXFObject("$DIMADEC", 70)]
	public System.Int16 DIMADEC {get; private set;}
	/// <summary>
    /// Alternate unit dimensioning performed if nonzero
    /// </summary>
	[DXFObject("$DIMALT", 70)]
	public System.Int16 DIMALT {get; private set;}
	/// <summary>
    /// Alternate unit decimal places
    /// </summary>
	[DXFObject("$DIMALTD", 70)]
	public System.Int16 DIMALTD {get; private set;}
	/// <summary>
    /// Alternate unit scale factor
    /// </summary>
	[DXFObject("$DIMALTF", 40)]
	public System.Double DIMALTF {get; private set;}
	/// <summary>
    /// Determines rounding of alternate units
    /// </summary>
	[DXFObject("$DIMALTRND", 40)]
	public System.Double DIMALTRND {get; private set;}
	/// <summary>
    /// Number of decimal places for tolerance values of an alternate
    /// units dimension
    /// </summary>
	[DXFObject("$DIMALTTD", 70)]
	public System.Int16 DIMALTTD {get; private set;}
	/// <summary>
    /// Controls suppression of zeros for alternate tolerance values:
    /// 0 = Suppresses zero feet and precisely zero inches
    /// 1 = Includes zero feet and precisely zero inches
    /// 2 = Includes zero feet and suppresses zero inches
    /// 3 = Includes zero inches and suppresses zero feet
    /// </summary>
	[DXFObject("$DIMALTTZ", 70)]
	public System.Int16 DIMALTTZ {get; private set;}
	/// <summary>
    /// Units format for alternate units of all dimension style family
    /// members except angular:
    /// 1 = Scientific; 2 = Decimal; 3 = Engineering;
    /// 4 = Architectural (stacked); 5 = Fractional (stacked);
    /// 6 = Architectural; 7 = Fractional
    /// </summary>
	[DXFObject("$DIMALTU", 70)]
	public System.Int16 DIMALTU {get; private set;}
	/// <summary>
    /// Controls suppression of zeros for alternate unit dimension
    /// values:
    /// 0 = Suppresses zero feet and precisely zero inches
    /// 1 = Includes zero feet and precisely zero inches
    /// 2 = Includes zero feet and suppresses zero inches
    /// 3 = Includes zero inches and suppresses zero feet
    /// </summary>
	[DXFObject("$DIMALTZ", 70)]
	public System.Int16 DIMALTZ {get; private set;}
	/// <summary>
    /// Alternate dimensioning suffix
    /// </summary>
	[DXFObject("$DIMAPOST", 1)]
	public System.String DIMAPOST {get; private set;}
	/// <summary>
    /// 1 = Create associative dimensioning
    /// 0 = Draw individual entities
    /// </summary>
	[DXFObject("$DIMASO", 70)]
	public System.Int16 DIMASO {get; private set;}
	/// <summary>
    /// Controls the associativity of dimension objects
    /// 0 = Creates exploded dimensions; there is no association
    /// between elements of the dimension, and the lines, arcs, arrowheads, and text of a dimension are drawn as separate
    /// objects
    /// 1 = Creates non-associative dimension objects; the elements
    /// of the dimension are formed into a single object, and if the
    /// definition point on the object moves, then the dimension
    /// value is updated
    /// 2 = Creates associative dimension objects; the elements of
    /// the dimension are formed into a single object and one or
    /// more definition points of the dimension are coupled with
    /// association points on geometric objects
    /// </summary>
	[DXFObject("$DIMASSOC", 280)]
	public System.IntPtr DIMASSOC {get; private set;}
	/// <summary>
    /// Dimensioning arrow size
    /// </summary>
	[DXFObject("$DIMASZ", 40)]
	public System.Double DIMASZ {get; private set;}
	/// <summary>
    /// Controls dimension text and arrow placement when space
    /// is not sufficient to place both within the extension lines:
    /// 0 = Places both text and arrows outside extension lines
    /// 1 = Moves arrows first, then text
    /// 2 = Moves text first, then arrows
    /// 3 = Moves either text or arrows, whichever fits best
    /// AutoCAD adds a leader to moved dimension text when
    /// DIMTMOVE is set to 1
    /// </summary>
	[DXFObject("$DIMATFIT", 70)]
	public System.Int16 DIMATFIT {get; private set;}
	/// <summary>
    /// Angle format for angular dimensions:
    /// 0 = Decimal degrees; 1 = Degrees/minutes/seconds;
    /// 2 = Gradians; 3 = Radians; 4 = Surveyor's units
    /// </summary>
	[DXFObject("$DIMAUNIT", 70)]
	public System.Int16 DIMAUNIT {get; private set;}
	/// <summary>
    /// Controls suppression of zeros for angular dimensions:
    /// 0 = Displays all leading and trailing zeros
    /// 1 = Suppresses leading zeros in decimal dimensions
    /// 2 = Suppresses trailing zeros in decimal dimensions
    /// 3 = Suppresses leading and trailing zeros
    /// </summary>
	[DXFObject("$DIMAZIN", 70)]
	public System.Int16 DIMAZIN {get; private set;}
	/// <summary>
    /// Arrow block name
    /// </summary>
	[DXFObject("$DIMBLK", 1)]
	public System.String DIMBLK {get; private set;}
	/// <summary>
    /// First arrow block name
    /// </summary>
	[DXFObject("$DIMBLK1", 1)]
	public System.String DIMBLK1 {get; private set;}
	/// <summary>
    /// Second arrow block name
    /// </summary>
	[DXFObject("$DIMBLK2", 1)]
	public System.String DIMBLK2 {get; private set;}
	/// <summary>
    /// Size of center mark/lines
    /// </summary>
	[DXFObject("$DIMCEN", 40)]
	public System.Double DIMCEN {get; private set;}
	/// <summary>
    /// Dimension line color:
    /// range is 0 = BYBLOCK; 256 = BYLAYER
    /// </summary>
	[DXFObject("$DIMCLRD", 70)]
	public System.Int16 DIMCLRD {get; private set;}
	/// <summary>
    /// Dimension extension line color:
    /// range is 0 = BYBLOCK; 256 = BYLAYER
    /// </summary>
	[DXFObject("$DIMCLRE", 70)]
	public System.Int16 DIMCLRE {get; private set;}
	/// <summary>
    /// Dimension text color:
    /// range is 0 = BYBLOCK; 256 = BYLAYER
    /// </summary>
	[DXFObject("$DIMCLRT", 70)]
	public System.Int16 DIMCLRT {get; private set;}
	/// <summary>
    /// Number of decimal places for the tolerance values of a
    /// primary units dimension
    /// </summary>
	[DXFObject("$DIMDEC", 70)]
	public System.Int16 DIMDEC {get; private set;}
	/// <summary>
    /// Dimension line extension
    /// </summary>
	[DXFObject("$DIMDLE", 40)]
	public System.Double DIMDLE {get; private set;}
	/// <summary>
    /// Dimension line increment
    /// </summary>
	[DXFObject("$DIMDLI", 40)]
	public System.Double DIMDLI {get; private set;}
	/// <summary>
    /// Single-character decimal separator used when creating dimensions
    /// whose unit format is decimal
    /// </summary>
	[DXFObject("$DIMDSEP", 70)]
	public System.Int16 DIMDSEP {get; private set;}
	/// <summary>
    /// Extension line extension
    /// </summary>
	[DXFObject("$DIMEXE", 40)]
	public System.Double DIMEXE {get; private set;}
	/// <summary>
    /// Extension line offset
    /// </summary>
	[DXFObject("$DIMEXO", 40)]
	public System.Double DIMEXO {get; private set;}
	/// <summary>
    /// Scale factor used to calculate the height of text for dimension
    /// fractions and tolerances. AutoCAD multiplies DIMTXT
    /// by DIMTFAC to set the fractional or tolerance text height
    /// </summary>
	[DXFObject("$DIMFAC", 40)]
	public System.Double DIMFAC {get; private set;}
	/// <summary>
    /// Dimension line gap
    /// </summary>
	[DXFObject("$DIMGAP", 40)]
	public System.Double DIMGAP {get; private set;}
	/// <summary>
    /// Horizontal dimension text position:
    /// 0 = Above dimension line and center-justified between extension
    /// lines
    /// 1 = Above dimension line and next to first extension line
    /// 2 = Above dimension line and next to second extension
    /// line
    /// 3 = Above and center-justified to first extension line
    /// 4 = Above and center-justified to second extension line
    /// </summary>
	[DXFObject("$DIMJUST", 70)]
	public System.Int16 DIMJUST {get; private set;}
	/// <summary>
    /// Arrow block name for leaders
    /// </summary>
	[DXFObject("$DIMLDRBLK", 1)]
	public System.String DIMLDRBLK {get; private set;}
	/// <summary>
    /// Linear measurements scale factor
    /// </summary>
	[DXFObject("$DIMLFAC", 40)]
	public System.Double DIMLFAC {get; private set;}
	/// <summary>
    /// Dimension limits generated if nonzero
    /// </summary>
	[DXFObject("$DIMLIM", 70)]
	public System.Int16 DIMLIM {get; private set;}
	/// <summary>
    /// Sets units for all dimension types except Angular:
    /// 1 = Scientific; 2 = Decimal; 3 = Engineering;
    /// 4 = Architectural; 5 = Fractional; 6 = Windows desktop
    /// </summary>
	[DXFObject("$DIMLUNIT", 70)]
	public System.Int16 DIMLUNIT {get; private set;}
	/// <summary>
    /// Dimension line lineweight:
    /// -3 = Standard
    /// -2 = ByLayer
    /// -1 = ByBlock
    /// 0-211 = an integer representing 100th of mm
    /// </summary>
	[DXFObject("$DIMLWD", 70)]
	public System.Int16 DIMLWD {get; private set;}
	/// <summary>
    /// Extension line lineweight:
    /// -3 = Standard
    /// -2 = ByLayer
    /// -1 = ByBlock
    /// 0-211 = an integer representing 100th of mm
    /// </summary>
	[DXFObject("$DIMLWE", 70)]
	public System.Int16 DIMLWE {get; private set;}
	/// <summary>
    /// General dimensioning suffix
    /// </summary>
	[DXFObject("$DIMPOST", 1)]
	public System.String DIMPOST {get; private set;}
	/// <summary>
    /// Rounding value for dimension distances
    /// </summary>
	[DXFObject("$DIMRND", 40)]
	public System.Double DIMRND {get; private set;}
	/// <summary>
    /// Use separate arrow blocks if nonzero
    /// </summary>
	[DXFObject("$DIMSAH", 70)]
	public System.Int16 DIMSAH {get; private set;}
	/// <summary>
    /// Overall dimensioning scale factor
    /// </summary>
	[DXFObject("$DIMSCALE", 40)]
	public System.Double DIMSCALE {get; private set;}
	/// <summary>
    /// Suppression of first extension line:
    /// 0 = Not suppressed; 1 = Suppressed
    /// </summary>
	[DXFObject("$DIMSD1", 70)]
	public System.Int16 DIMSD1 {get; private set;}
	/// <summary>
    /// Suppression of second extension line:
    /// 0 = Not suppressed; 1 = Suppressed
    /// </summary>
	[DXFObject("$DIMSD2", 70)]
	public System.Int16 DIMSD2 {get; private set;}
	/// <summary>
    /// First extension line suppressed if nonzero
    /// </summary>
	[DXFObject("$DIMSE1", 70)]
	public System.Int16 DIMSE1 {get; private set;}
	/// <summary>
    /// Second extension line suppressed if nonzero
    /// </summary>
	[DXFObject("$DIMSE2", 70)]
	public System.Int16 DIMSE2 {get; private set;}
	/// <summary>
    /// 1 = Recompute dimensions while dragging
    /// 0 = Drag original image
    /// </summary>
	[DXFObject("$DIMSHO", 70)]
	public System.Int16 DIMSHO {get; private set;}
	/// <summary>
    /// Suppress outside-extensions dimension lines if nonzero
    /// </summary>
	[DXFObject("$DIMSOXD", 70)]
	public System.Int16 DIMSOXD {get; private set;}
	/// <summary>
    /// Dimension style name
    /// </summary>
	[DXFObject("$DIMSTYLE", 2)]
	public System.String DIMSTYLE {get; private set;}
	/// <summary>
    /// Text above dimension line if nonzero
    /// </summary>
	[DXFObject("$DIMTAD", 70)]
	public System.Int16 DIMTAD {get; private set;}
	/// <summary>
    /// Number of decimal places to display the tolerance values
    /// </summary>
	[DXFObject("$DIMTDEC", 70)]
	public System.Int16 DIMTDEC {get; private set;}
	/// <summary>
    /// Dimension tolerance display scale factor
    /// </summary>
	[DXFObject("$DIMTFAC", 40)]
	public System.Double DIMTFAC {get; private set;}
	/// <summary>
    /// Text inside horizontal if nonzero
    /// </summary>
	[DXFObject("$DIMTIH", 70)]
	public System.Int16 DIMTIH {get; private set;}
	/// <summary>
    /// Force text inside extensions if nonzero
    /// </summary>
	[DXFObject("$DIMTIX", 70)]
	public System.Int16 DIMTIX {get; private set;}
	/// <summary>
    /// Minus tolerance
    /// </summary>
	[DXFObject("$DIMTM", 40)]
	public System.Double DIMTM {get; private set;}
	/// <summary>
    /// Dimension text movement rules:
    /// 0 = Moves the dimension line with dimension text
    /// 1 = Adds a leader when dimension text is moved
    /// 2 = Allows text to be moved freely without a leader
    /// </summary>
	[DXFObject("$DIMTMOVE", 70)]
	public System.Int16 DIMTMOVE {get; private set;}
	/// <summary>
    /// If text is outside extensions, force line extensions between
    /// extensions if nonzero
    /// </summary>
	[DXFObject("$DIMTOFL", 70)]
	public System.Int16 DIMTOFL {get; private set;}
	/// <summary>
    /// Text outside horizontal if nonzero
    /// </summary>
	[DXFObject("$DIMTOH", 70)]
	public System.Int16 DIMTOH {get; private set;}
	/// <summary>
    /// Dimension tolerances generated if nonzero
    /// </summary>
	[DXFObject("$DIMTOL", 70)]
	public System.Int16 DIMTOL {get; private set;}
	/// <summary>
    /// Vertical justification for tolerance values:
    /// 0 = Top; 1 = Middle; 2 = Bottom
    /// </summary>
	[DXFObject("$DIMTOLJ", 70)]
	public System.Int16 DIMTOLJ {get; private set;}
	/// <summary>
    /// Plus tolerance
    /// </summary>
	[DXFObject("$DIMTP", 40)]
	public System.Double DIMTP {get; private set;}
	/// <summary>
    /// Dimensioning tick size:
    /// 0 = No ticks
    /// </summary>
	[DXFObject("$DIMTSZ", 40)]
	public System.Double DIMTSZ {get; private set;}
	/// <summary>
    /// Text vertical position
    /// </summary>
	[DXFObject("$DIMTVP", 40)]
	public System.Double DIMTVP {get; private set;}
	/// <summary>
    /// Dimension text style
    /// </summary>
	[DXFObject("$DIMTXSTY", 7)]
	public System.String DIMTXSTY {get; private set;}
	/// <summary>
    /// Dimensioning text height
    /// </summary>
	[DXFObject("$DIMTXT", 40)]
	public System.Double DIMTXT {get; private set;}
	/// <summary>
    /// Controls suppression of zeros for tolerance values:
    /// 0 = Suppresses zero feet and precisely zero inches
    /// 1 = Includes zero feet and precisely zero inches
    /// 2 = Includes zero feet and suppresses zero inches
    /// 3 = Includes zero inches and suppresses zero feet
    /// </summary>
	[DXFObject("$DIMTZIN", 70)]
	public System.Int16 DIMTZIN {get; private set;}
	/// <summary>
    /// Cursor functionality for user-positioned text:
    /// 0 = Controls only the dimension line location
    /// 1 = Controls the text position as well as the dimension line
    /// location
    /// </summary>
	[DXFObject("$DIMUPT", 70)]
	public System.Int16 DIMUPT {get; private set;}
	/// <summary>
    /// Controls suppression of zeros for primary unit values:
    /// 0 = Suppresses zero feet and precisely zero inches
    /// 1 = Includes zero feet and precisely zero inches
    /// 2 = Includes zero feet and suppresses zero inches
    /// 3 = Includes zero inches and suppresses zero feet
    /// </summary>
	[DXFObject("$DIMZIN", 70)]
	public System.Int16 DIMZIN {get; private set;}
	/// <summary>
    /// Controls the display of silhouette curves of body objects in
    /// Wireframe mode:
    /// 0 = Off; 1 = On
    /// </summary>
	[DXFObject("$DISPSILH", 70)]
	public System.Int16 DISPSILH {get; private set;}
	/// <summary>
    /// Hard-pointer ID to visual style while creating 3D solid
    /// primitives. The defualt value is NULL
    /// </summary>
	[DXFObject("$DRAGVS", 349)]
	public System.IntPtr DRAGVS {get; private set;}
	/// <summary>
    /// Drawing code page; set to the system code page when a
    /// new drawing is created, but not otherwise maintained by
    /// AutoCAD
    /// </summary>
	[DXFObject("$DWGCODEPAGE", 3)]
	public System.String DWGCODEPAGE {get; private set;}
	/// <summary>
    /// Current elevation set by ELEV command
    /// </summary>
	[DXFObject("$ELEVATION", 40)]
	public System.Double ELEVATION {get; private set;}
	/// <summary>
    /// Lineweight endcaps setting for new objects:
    /// 0 = none; 1 = round; 2 = angle; 3 = square
    /// </summary>
	[DXFObject("$ENDCAPS", 280)]
	public System.IntPtr ENDCAPS {get; private set;}
	/// <summary>
    /// X, Y, and Z drawing extents upper-right corner (in WCS)
    /// </summary>
	[DXFObject("$EXTMAX", 10,20,30)]
	public System.IntPtr EXTMAX {get; private set;}
	/// <summary>
    /// X, Y, and Z drawing extents upper-right corner (in WCS)
    /// </summary>
	[DXFObject("$EXTMIN", 10,20,30)]
	public System.IntPtr EXTMIN {get; private set;}
	/// <summary>
    /// Controls symbol table naming:
    /// 0 = Release 14 compatibility. Limits names to 31 characters
    /// in length. Names can include the letters A to Z, the numerals
    /// 0 to 9, and the special characters dollar sign ($), underscore
    /// (_), and hyphen (-).
    /// 1 = AutoCAD 2000. Names can be up to 255 characters in
    /// length, and can include the letters A to Z, the numerals 0
    /// to 9, spaces, and any special characters not used for other
    /// purposes by Microsoft Windows and AutoCAD
    /// </summary>
	[DXFObject("$EXTNAMES", 290)]
	public System.IntPtr EXTNAMES {get; private set;}
	/// <summary>
    /// Fillet radius
    /// </summary>
	[DXFObject("$FILLETRAD", 40)]
	public System.Double FILLETRAD {get; private set;}
	/// <summary>
    /// Fill mode on if nonzero
    /// </summary>
	[DXFObject("$FILLMODE", 70)]
	public System.Int16 FILLMODE {get; private set;}
	/// <summary>
    /// Set at creation time, uniquely identifies a particular drawing
    /// </summary>
	[DXFObject("$FINGERPRINTGUID", 2)]
	public System.String FINGERPRINTGUID {get; private set;}
	/// <summary>
    /// Specifies a gap to be displayed where an object is hidden
    /// by another object; the value is specified as a percent of one
    /// unit and is independent of the zoom level. A haloed line is
    /// shortened at the point where it is hidden when HIDE or
    /// the Hidden option of SHADEMODE is used
    /// </summary>
	[DXFObject("$HALOGAP", 280)]
	public System.IntPtr HALOGAP {get; private set;}
	/// <summary>
    /// Next available handle
    /// </summary>
	[DXFObject("$HANDSEED", 5)]
	public System.String HANDSEED {get; private set;}
	/// <summary>
    /// Specifies HIDETEXT system variable:
    /// 0 = HIDE ignores text objects when producing the hidden
    /// view
    /// 1 = HIDE does not ignore text objects
    /// </summary>
	[DXFObject("$HIDETEXT", 290)]
	public System.IntPtr HIDETEXT {get; private set;}
	/// <summary>
    /// Path for all relative hyperlinks in the drawing. If null, the
    /// drawing path is used
    /// </summary>
	[DXFObject("$HYPERLINKBASE", 1)]
	public System.String HYPERLINKBASE {get; private set;}
	/// <summary>
    /// Controls whether layer and spatial indexes are created and
    /// saved in drawing files:
    /// 0 = No indexes are created
    /// 1 = Layer index is created
    /// 2 = Spatial index is created
    /// 3 = Layer and spatial indexes are created
    /// </summary>
	[DXFObject("$INDEXCTL", 280)]
	public System.IntPtr INDEXCTL {get; private set;}
	/// <summary>
    /// Insertion base set by BASE command (in WCS)
    /// </summary>
	[DXFObject("$INSBASE", 10,20,30)]
	public System.IntPtr INSBASE {get; private set;}
	/// <summary>
    /// Default drawing units for AutoCAD DesignCenter blocks:
    /// 0 = Unitless; 1 = Inches; 2 = Feet; 3 = Miles; 4 = Millimeters;
    /// 5 = Centimeters; 6 = Meters; 7 = Kilometers; 8 = Microinches;
    /// 9 = Mils; 10 = Yards; 11 = Angstroms; 12 = Nanometers;
    /// 13 = Microns; 14 = Decimeters; 15 = Decameters;
    /// 16 = Hectometers; 17 = Gigameters; 18 = Astronomical
    /// units;
    /// 19 = Light years; 20 = Parsecs
    /// </summary>
	[DXFObject("$INSUNITS", 70)]
	public System.Int16 INSUNITS {get; private set;}
	/// <summary>
    /// Represents the ACI color index of the 'interference objects'
    /// created during the interfere command.Default value is 1
    /// </summary>
	[DXFObject("$INTERFERECOLOR", 62)]
	public System.Int16 INTERFERECOLOR {get; private set;}
	/// <summary>
    /// Hard-pointer ID to the visual style for interference objects.
    /// Default visual style is Conceptual.
    /// </summary>
	[DXFObject("$INTERFEREOBJVS", 345)]
	public System.IntPtr INTERFEREOBJVS {get; private set;}
	/// <summary>
    /// Hard-pointer ID to the visual style for the viewport during
    /// interference checking. Default visual style is 3d Wireframe.
    /// </summary>
	[DXFObject("$INTERFEREVPVS", 346)]
	public System.IntPtr INTERFEREVPVS {get; private set;}
	/// <summary>
    /// Specifies the entity color of intersection polylines:
    /// Values 1-255 designate an AutoCAD color index (ACI)
    /// 0 = Color BYBLOCK
    /// 256 = Color BYLAYER
    /// 257 = Color BYENTITY
    /// </summary>
	[DXFObject("$INTERSECTIONCOLOR", 70)]
	public System.Int16 INTERSECTIONCOLOR {get; private set;}
	/// <summary>
    /// Specifies the display of intersection polylines:
    /// 0 = Turns off the display of intersection polylines
    /// 1 = Turns on the display of intersection polylines
    /// </summary>
	[DXFObject("$INTERSECTIONDISPLAY", 290)]
	public System.IntPtr INTERSECTIONDISPLAY {get; private set;}
	/// <summary>
    /// Lineweight joint setting for new objects:
    /// 0=none; 1= round; 2 = angle; 3 = flat
    /// </summary>
	[DXFObject("$JOINSTYLE", 280)]
	public System.IntPtr JOINSTYLE {get; private set;}
	/// <summary>
    /// Nonzero if limits checking is on
    /// </summary>
	[DXFObject("$LIMCHECK", 70)]
	public System.Int16 LIMCHECK {get; private set;}
	/// <summary>
    /// XY drawing limits upper-right corner (in WCS)
    /// </summary>
	[DXFObject("$LIMMAX", 10,20)]
	public System.IntPtr LIMMAX {get; private set;}
	/// <summary>
    /// XY drawing limits lower-left corner (in WCS)
    /// </summary>
	[DXFObject("$LIMMIN", 10,20)]
	public System.IntPtr LIMMIN {get; private set;}
	/// <summary>
    /// Global linetype scale
    /// </summary>
	[DXFObject("$LTSCALE", 40)]
	public System.Double LTSCALE {get; private set;}
	/// <summary>
    /// Units format for coordinates and distances
    /// </summary>
	[DXFObject("$LUNITS", 70)]
	public System.Int16 LUNITS {get; private set;}
	/// <summary>
    /// Units precision for coordinates and distances
    /// </summary>
	[DXFObject("$LUPREC", 70)]
	public System.Int16 LUPREC {get; private set;}
	/// <summary>
    /// Controls the display of lineweights on the Model or Layout
    /// tab:
    /// 0 = Lineweight is not displayed
    /// 1 = Lineweight is displayed
    /// </summary>
	[DXFObject("$LWDISPLAY", 290)]
	public System.IntPtr LWDISPLAY {get; private set;}
	/// <summary>
    /// Sets maximum number of viewports to be regenerated
    /// </summary>
	[DXFObject("$MAXACTVP", 70)]
	public System.Int16 MAXACTVP {get; private set;}
	/// <summary>
    /// Sets drawing units: 0 = English; 1 = Metric
    /// </summary>
	[DXFObject("$MEASUREMENT", 70)]
	public System.Int16 MEASUREMENT {get; private set;}
	/// <summary>
    /// Name of menu file
    /// </summary>
	[DXFObject("$MENU", 1)]
	public System.String MENU {get; private set;}
	/// <summary>
    /// Mirror text if nonzero
    /// </summary>
	[DXFObject("$MIRRTEXT", 70)]
	public System.Int16 MIRRTEXT {get; private set;}
	/// <summary>
    /// Specifies the color of obscured lines. An obscured line is a
    /// hidden line made visible by changing its color and linetype
    /// and is visible only when the HIDE or SHADEMODE command
    /// is used. The OBSCUREDCOLOR setting is visible only
    /// if the OBSCUREDLTYPE is turned ON by setting it to a value
    /// other than 0.
    /// 0 and 256 = Entity color
    /// 1-255 = An AutoCAD color index (ACI)
    /// </summary>
	[DXFObject("$OBSCOLOR", 70)]
	public System.Int16 OBSCOLOR {get; private set;}
	/// <summary>
    /// Specifies the linetype of obscured lines. Obscured linetypes
    /// are independent of zoom level, unlike regular AutoCAD
    /// linetypes. Value 0 turns off display of obscured lines and is
    /// the default. Linetype values are defined as follows:
    /// 0 = Off
    /// 1 = Solid
    /// 2 = Dashed
    /// 3 = Dotted
    /// 4 = Short Dash
    /// 5 = Medium Dash
    /// 6 = Long Dash
    /// 7 = Double Short Dash
    /// 8 = Double Medium Dash
    /// 9 = Double Long Dash
    /// 10 = Medium Long Dash
    /// 11 = Sparse Dot
    /// </summary>
	[DXFObject("$OBSLTYPE", 280)]
	public System.IntPtr OBSLTYPE {get; private set;}
	/// <summary>
    /// Ortho mode on if nonzero
    /// </summary>
	[DXFObject("$ORTHOMODE", 70)]
	public System.Int16 ORTHOMODE {get; private set;}
	/// <summary>
    /// Point display mode
    /// </summary>
	[DXFObject("$PDMODE", 70)]
	public System.Int16 PDMODE {get; private set;}
	/// <summary>
    /// Point display size
    /// </summary>
	[DXFObject("$PDSIZE", 40)]
	public System.Double PDSIZE {get; private set;}
	/// <summary>
    /// Current paper space elevation
    /// </summary>
	[DXFObject("$PELEVATION", 40)]
	public System.Double PELEVATION {get; private set;}
	/// <summary>
    /// Maximum X, Y, and Z extents for paper space
    /// </summary>
	[DXFObject("$PEXTMAX", 10,20,30)]
	public System.IntPtr PEXTMAX {get; private set;}
	/// <summary>
    /// Minimum X, Y, and Z extents for paper space
    /// </summary>
	[DXFObject("$PEXTMIN", 10,20,30)]
	public System.IntPtr PEXTMIN {get; private set;}
	/// <summary>
    /// Paper space insertion base point
    /// </summary>
	[DXFObject("$PINSBASE", 10,20,30)]
	public System.IntPtr PINSBASE {get; private set;}
	/// <summary>
    /// Limits checking in paper space when nonzero
    /// </summary>
	[DXFObject("$PLIMCHECK", 70)]
	public System.Int16 PLIMCHECK {get; private set;}
	/// <summary>
    /// Maximum X and Y limits in paper space
    /// </summary>
	[DXFObject("$PLIMMAX", 10,20)]
	public System.IntPtr PLIMMAX {get; private set;}
	/// <summary>
    /// Minimum X and Y limits in paper space
    /// </summary>
	[DXFObject("$PLIMMIN", 10,20)]
	public System.IntPtr PLIMMIN {get; private set;}
	/// <summary>
    /// Governs the generation of linetype patterns around the
    /// vertices of a 2D polyline:
    /// 1 = Linetype is generated in a continuous pattern around
    /// vertices of the polyline
    /// 0 = Each segment of the polyline starts and ends with a
    /// dash
    /// </summary>
	[DXFObject("$PLINEGEN", 70)]
	public System.Int16 PLINEGEN {get; private set;}
	/// <summary>
    /// Default polyline width
    /// </summary>
	[DXFObject("$PLINEWID", 40)]
	public System.Double PLINEWID {get; private set;}
	/// <summary>
    /// Assigns a project name to the current drawing. Used when an external reference or image is not found on its original
    /// path. The project name points to a section in the registry
    /// that can contain one or more search paths for each project name defined. Project names and their search directories
    /// are created from the Files tab of the Options dialog box
    /// </summary>
	[DXFObject("$PROJECTNAME", 1)]
	public System.String PROJECTNAME {get; private set;}
	/// <summary>
    /// Controls the saving of proxy object images
    /// </summary>
	[DXFObject("$PROXYGRAPHICS", 70)]
	public System.Int16 PROXYGRAPHICS {get; private set;}
	/// <summary>
    /// Controls paper space linetype scaling:
    /// 1 = No special linetype scaling
    /// 0 = Viewport scaling governs linetype scaling
    /// </summary>
	[DXFObject("$PSLTSCALE", 70)]
	public System.Int16 PSLTSCALE {get; private set;}
	/// <summary>
    /// Indicates whether the current drawing is in a Color-Dependent
    /// or Named Plot Style mode:
    /// 0 = Uses named plot style tables in the current drawing
    /// 1 = Uses color-dependent plot style tables in the current
    /// drawing
    /// </summary>
	[DXFObject("$PSTYLEMODE", 290)]
	public System.IntPtr PSTYLEMODE {get; private set;}
	/// <summary>
    /// View scale factor for new viewports:
    /// 0 = Scaled to fit
    /// >0 = Scale factor (a positive real value)
    /// </summary>
	[DXFObject("$PSVPSCALE", 40)]
	public System.Double PSVPSCALE {get; private set;}
	/// <summary>
    /// Name of the UCS that defines the origin and orientation
    /// of orthographic UCS settings (paper space only)
    /// </summary>
	[DXFObject("$PUCSBASE", 2)]
	public System.String PUCSBASE {get; private set;}
	/// <summary>
    /// Current paper space UCS name
    /// </summary>
	[DXFObject("$PUCSNAME", 2)]
	public System.String PUCSNAME {get; private set;}
	/// <summary>
    /// Current paper space UCS origin
    /// </summary>
	[DXFObject("$PUCSORG", 10,20,30)]
	public System.IntPtr PUCSORG {get; private set;}
	/// <summary>
    /// Point which becomes the new UCS origin after changing
    /// paper space UCS to BACK when PUCSBASE is set to WORLD
    /// </summary>
	[DXFObject("$PUCSORGBACK", 10,20,30)]
	public System.IntPtr PUCSORGBACK {get; private set;}
	/// <summary>
    /// Point which becomes the new UCS origin after changing
    /// paper space UCS to BOTTOM when PUCSBASE is set to
    /// WORLD
    /// </summary>
	[DXFObject("$PUCSORGBOTTOM", 10,20,30)]
	public System.IntPtr PUCSORGBOTTOM {get; private set;}
	/// <summary>
    /// Point which becomes the new UCS origin after changing
    /// paper space UCS to FRONT when PUCSBASE is set to
    /// WORLD
    /// </summary>
	[DXFObject("$PUCSORGFRONT", 10,20,30)]
	public System.IntPtr PUCSORGFRONT {get; private set;}
	/// <summary>
    /// Point which becomes the new UCS origin after changing
    /// paper space UCS to LEFT when PUCSBASE is set to WORLD
    /// </summary>
	[DXFObject("$PUCSORGLEFT", 10,20,30)]
	public System.IntPtr PUCSORGLEFT {get; private set;}
	/// <summary>
    /// Point which becomes the new UCS origin after changing
    /// paper space UCS to RIGHT when PUCSBASE is set to
    /// WORLD
    /// </summary>
	[DXFObject("$PUCSORGRIGHT", 10,20,30)]
	public System.IntPtr PUCSORGRIGHT {get; private set;}
	/// <summary>
    /// Point which becomes the new UCS origin after changing
    /// paper space UCS to TOP when PUCSBASE is set to WORLD
    /// </summary>
	[DXFObject("$PUCSORGTOP", 10,20,30)]
	public System.IntPtr PUCSORGTOP {get; private set;}
	/// <summary>
    /// If paper space UCS is orthographic (PUCSORTHOVIEW not
    /// equal to 0), this is the name of the UCS that the orthographic
    /// UCS is relative to. If blank, UCS is relative to WORLD
    /// </summary>
	[DXFObject("$PUCSORTHOREF", 2)]
	public System.String PUCSORTHOREF {get; private set;}
	/// <summary>
    /// Orthographic view type of paper space UCS:
    /// 0 = UCS is not orthographic;
    /// 1 = Top; 2 = Bottom;
    /// 3 = Front; 4 = Back;
    /// 5 = Left; 6 = Right
    /// </summary>
	[DXFObject("$PUCSORTHOVIEW", 70)]
	public System.Int16 PUCSORTHOVIEW {get; private set;}
	/// <summary>
    /// Current paper space UCS X axis
    /// </summary>
	[DXFObject("$PUCSXDIR", 10,20,30)]
	public System.IntPtr PUCSXDIR {get; private set;}
	/// <summary>
    /// Current paper space UCS Y axis
    /// </summary>
	[DXFObject("$PUCSYDIR", 10,20,30)]
	public System.IntPtr PUCSYDIR {get; private set;}
	/// <summary>
    /// Quick Text mode on if nonzero
    /// </summary>
	[DXFObject("$QTEXTMODE", 70)]
	public System.Int16 QTEXTMODE {get; private set;}
	/// <summary>
    /// REGENAUTO mode on if nonzero
    /// </summary>
	[DXFObject("$REGENMODE", 70)]
	public System.Int16 REGENMODE {get; private set;}
	/// <summary>
    /// 0 = Faces shaded, edges not highlighted
    /// 1 = Faces shaded, edges highlighted in black
    /// 2 = Faces not filled, edges in entity color
    /// 3 = Faces in entity color, edges in black
    /// </summary>
	[DXFObject("$SHADEDGE", 70)]
	public System.Int16 SHADEDGE {get; private set;}
	/// <summary>
    /// Percent ambient/diffuse light; range 1-100; default 70
    /// </summary>
	[DXFObject("$SHADEDIF", 70)]
	public System.Int16 SHADEDIF {get; private set;}
	/// <summary>
    /// Location of the ground shadow plane. This is a Z axis ordinate.
    /// </summary>
	[DXFObject("$SHADOWPLANELOCATION", 40)]
	public System.Double SHADOWPLANELOCATION {get; private set;}
	/// <summary>
    /// Sketch record increment
    /// </summary>
	[DXFObject("$SKETCHINC", 40)]
	public System.Double SKETCHINC {get; private set;}
	/// <summary>
    /// 0 = Sketch lines; 1 = Sketch polylines
    /// </summary>
	[DXFObject("$SKPOLY", 70)]
	public System.Int16 SKPOLY {get; private set;}
	/// <summary>
    /// Controls the object sorting methods; accessible from the
    /// Options dialog box User Preferences tab. SORTENTS uses
    /// the following bitcodes:
    /// 0 = Disables SORTENTS
    /// 1 = Sorts for object selection
    /// 2 = Sorts for object snap
    /// 4 = Sorts for redraws
    /// 8 = Sorts for MSLIDE command slide creation
    /// 16 = Sorts for REGEN commands
    /// 32 = Sorts for plotting
    /// 64 = Sorts for PostScript output
    /// </summary>
	[DXFObject("$SORTENTS", 280)]
	public System.IntPtr SORTENTS {get; private set;}
	/// <summary>
    /// Spline control polygon display: 1 = On; 0 = Off
    /// </summary>
	[DXFObject("$SPLFRAME", 70)]
	public System.Int16 SPLFRAME {get; private set;}
	/// <summary>
    /// Number of line segments per spline patch
    /// </summary>
	[DXFObject("$SPLINESEGS", 70)]
	public System.Int16 SPLINESEGS {get; private set;}
	/// <summary>
    /// Spline curve type for PEDIT Spline
    /// </summary>
	[DXFObject("$SPLINETYPE", 70)]
	public System.Int16 SPLINETYPE {get; private set;}
	/// <summary>
    /// Number of mesh tabulations in first direction
    /// </summary>
	[DXFObject("$SURFTAB1", 70)]
	public System.Int16 SURFTAB1 {get; private set;}
	/// <summary>
    /// Number of mesh tabulations in second direction
    /// </summary>
	[DXFObject("$SURFTAB2", 70)]
	public System.Int16 SURFTAB2 {get; private set;}
	/// <summary>
    /// Surface type for PEDIT Smooth
    /// </summary>
	[DXFObject("$SURFTYPE", 70)]
	public System.Int16 SURFTYPE {get; private set;}
	/// <summary>
    /// Surface density (for PEDIT Smooth) in M direction
    /// </summary>
	[DXFObject("$SURFU", 70)]
	public System.Int16 SURFU {get; private set;}
	/// <summary>
    /// Surface density (for PEDIT Smooth) in N direction
    /// </summary>
	[DXFObject("$SURFV", 70)]
	public System.Int16 SURFV {get; private set;}
	/// <summary>
    /// Local date/time of drawing creation (see �Special Handling
    /// of Date/Time Variables�)
    /// </summary>
	[DXFObject("$TDCREATE", 40)]
	public System.Double TDCREATE {get; private set;}
	/// <summary>
    /// Cumulative editing time for this drawing (see �Special
    /// Handling of Date/Time Variables�)
    /// </summary>
	[DXFObject("$TDINDWG", 40)]
	public System.Double TDINDWG {get; private set;}
	/// <summary>
    /// Universal date/time the drawing was created (see �Special
    /// Handling of Date/Time Variables�)
    /// </summary>
	[DXFObject("$TDUCREATE", 40)]
	public System.Double TDUCREATE {get; private set;}
	/// <summary>
    /// Local date/time of last drawing update (see �Special
    /// Handling of Date/Time Variables�)
    /// </summary>
	[DXFObject("$TDUPDATE", 40)]
	public System.Double TDUPDATE {get; private set;}
	/// <summary>
    /// User-elapsed timer
    /// </summary>
	[DXFObject("$TDUSRTIMER", 40)]
	public System.Double TDUSRTIMER {get; private set;}
	/// <summary>
    /// Universal date/time of the last update/save (see �Special
    /// Handling of Date/Time Variables�)
    /// </summary>
	[DXFObject("$TDUUPDATE", 40)]
	public System.Double TDUUPDATE {get; private set;}
	/// <summary>
    /// Default text height
    /// </summary>
	[DXFObject("$TEXTSIZE", 40)]
	public System.Double TEXTSIZE {get; private set;}
	/// <summary>
    /// Current text style name
    /// </summary>
	[DXFObject("$TEXTSTYLE", 7)]
	public System.String TEXTSTYLE {get; private set;}
	/// <summary>
    /// Current thickness set by ELEV command
    /// </summary>
	[DXFObject("$THICKNESS", 40)]
	public System.Double THICKNESS {get; private set;}
	/// <summary>
    /// 1 for previous release compatibility mode; 0 otherwise
    /// </summary>
	[DXFObject("$TILEMODE", 70)]
	public System.Int16 TILEMODE {get; private set;}
	/// <summary>
    /// Default trace width
    /// </summary>
	[DXFObject("$TRACEWID", 40)]
	public System.Double TRACEWID {get; private set;}
	/// <summary>
    /// Specifies the maximum depth of the spatial index
    /// </summary>
	[DXFObject("$TREEDEPTH", 70)]
	public System.Int16 TREEDEPTH {get; private set;}
	/// <summary>
    /// Name of the UCS that defines the origin and orientation
    /// of orthographic UCS settings
    /// </summary>
	[DXFObject("$UCSBASE", 2)]
	public System.String UCSBASE {get; private set;}
	/// <summary>
    /// Name of current UCS
    /// </summary>
	[DXFObject("$UCSNAME", 2)]
	public System.String UCSNAME {get; private set;}
	/// <summary>
    /// Origin of current UCS (in WCS)
    /// </summary>
	[DXFObject("$UCSORG", 10,20,30)]
	public System.IntPtr UCSORG {get; private set;}
	/// <summary>
    /// Point which becomes the new UCS origin after changing
    /// model space UCS to BACK when UCSBASE is set to WORLD
    /// </summary>
	[DXFObject("$UCSORGBACK", 10,20,30)]
	public System.IntPtr UCSORGBACK {get; private set;}
	/// <summary>
    /// Point which becomes the new UCS origin after changing
    /// model space UCS to BOTTOM when UCSBASE is set to
    /// WORLD
    /// </summary>
	[DXFObject("$UCSORGBOTTOM", 10,20,30)]
	public System.IntPtr UCSORGBOTTOM {get; private set;}
	/// <summary>
    /// Point which becomes the new UCS origin after changing
    /// model space UCS to FRONT when UCSBASE is set to
    /// WORLD
    /// </summary>
	[DXFObject("$UCSORGFRONT", 10,20,30)]
	public System.IntPtr UCSORGFRONT {get; private set;}
	/// <summary>
    /// Point which becomes the new UCS origin after changing
    /// model space UCS to LEFT when UCSBASE is set to WORLD
    /// </summary>
	[DXFObject("$UCSORGLEFT", 10,20,30)]
	public System.IntPtr UCSORGLEFT {get; private set;}
	/// <summary>
    /// Point which becomes the new UCS origin after changing
    /// model space UCS to RIGHT when UCSBASE is set to WORLD
    /// </summary>
	[DXFObject("$UCSORGRIGHT", 10,20,30)]
	public System.IntPtr UCSORGRIGHT {get; private set;}
	/// <summary>
    /// Point which becomes the new UCS origin after changing
    /// model space UCS to TOP when UCSBASE is set to WORLD
    /// </summary>
	[DXFObject("$UCSORGTOP", 10,20,30)]
	public System.IntPtr UCSORGTOP {get; private set;}
	/// <summary>
    /// If model space UCS is orthographic (UCSORTHOVIEW not
    /// equal to 0), this is the name of the UCS that the orthographic
    /// UCS is relative to. If blank, UCS is relative to WORLD
    /// </summary>
	[DXFObject("$UCSORTHOREF", 2)]
	public System.String UCSORTHOREF {get; private set;}
	/// <summary>
    /// Orthographic view type of model space UCS:
    /// 0 = UCS is not orthographic;
    /// 1 = Top; 2 = Bottom;
    /// 3 = Front; 4 = Back;
    /// 5 = Left; 6 = Right
    /// </summary>
	[DXFObject("$UCSORTHOVIEW", 70)]
	public System.Int16 UCSORTHOVIEW {get; private set;}
	/// <summary>
    /// Direction of the current UCS X axis (in WCS)
    /// </summary>
	[DXFObject("$UCSXDIR", 10,20,30)]
	public System.IntPtr UCSXDIR {get; private set;}
	/// <summary>
    /// Direction of the current UCS Y axis (in WCS)
    /// </summary>
	[DXFObject("$UCSYDIR", 10,20,30)]
	public System.IntPtr UCSYDIR {get; private set;}
	/// <summary>
    /// Low bit set = Display fractions, feet-and-inches, and surveyor's
    /// angles in input format
    /// </summary>
	[DXFObject("$UNITMODE", 70)]
	public System.Int16 UNITMODE {get; private set;}
	/// <summary>
    /// Five integer variables intended for use by third-party developers
    /// </summary>
	[DXFObject("$USERI1", 70)]
	public System.Int16 USERI1 {get; private set;}
	/// <summary>
    /// Five integer variables intended for use by third-party developers
    /// </summary>
	[DXFObject("$USERI2", 70)]
	public System.Int16 USERI2 {get; private set;}
	/// <summary>
    /// Five integer variables intended for use by third-party developers
    /// </summary>
	[DXFObject("$USERI3", 70)]
	public System.Int16 USERI3 {get; private set;}
	/// <summary>
    /// Five integer variables intended for use by third-party developers
    /// </summary>
	[DXFObject("$USERI4", 70)]
	public System.Int16 USERI4 {get; private set;}
	/// <summary>
    /// Five integer variables intended for use by third-party developers
    /// </summary>
	[DXFObject("$USERI5", 70)]
	public System.Int16 USERI5 {get; private set;}
	/// <summary>
    /// Five real variables intended for use by third-party developers
    /// </summary>
	[DXFObject("$USERR1", 40)]
	public System.Double USERR1 {get; private set;}
	/// <summary>
    /// Five real variables intended for use by third-party developers
    /// </summary>
	[DXFObject("$USERR2", 40)]
	public System.Double USERR2 {get; private set;}
	/// <summary>
    /// Five real variables intended for use by third-party developers
    /// </summary>
	[DXFObject("$USERR3", 40)]
	public System.Double USERR3 {get; private set;}
	/// <summary>
    /// Five real variables intended for use by third-party developers
    /// </summary>
	[DXFObject("$USERR4", 40)]
	public System.Double USERR4 {get; private set;}
	/// <summary>
    /// Five real variables intended for use by third-party developers
    /// </summary>
	[DXFObject("$USERR5", 40)]
	public System.Double USERR5 {get; private set;}
	/// <summary>
    /// 0 = Timer off; 1 = Timer on
    /// </summary>
	[DXFObject("$USRTIMER", 70)]
	public System.Int16 USRTIMER {get; private set;}
	/// <summary>
    /// Uniquely identifies a particular version of a drawing. Updated
    /// when the drawing is modified
    /// </summary>
	[DXFObject("$VERSIONGUID", 2)]
	public System.String VERSIONGUID {get; private set;}
	/// <summary>
    /// 0 = Don't retain xref-dependent visibility settings
    /// 1 = Retain xref-dependent visibility settings
    /// </summary>
	[DXFObject("$VISRETAIN", 70)]
	public System.Int16 VISRETAIN {get; private set;}
	/// <summary>
    /// 1 = Set UCS to WCS during DVIEW/VPOINT
    /// 0 = Don't change UCS
    /// </summary>
	[DXFObject("$WORLDVIEW", 70)]
	public System.Int16 WORLDVIEW {get; private set;}
	/// <summary>
    /// Controls the visibility of xref clipping boundaries:
    /// 0 = Clipping boundary is not visible
    /// 1 = Clipping boundary is visible
    /// </summary>
	[DXFObject("$XCLIPFRAME", 290)]
	public System.IntPtr XCLIPFRAME {get; private set;}
	/// <summary>
    /// Controls whether the current drawing can be edited inplace
    /// when being referenced by another drawing.
    /// 0 = Can't use in-place reference editing
    /// 1 = Can use in-place reference editing
    /// </summary>
	[DXFObject("$XEDIT", 290)]
	public System.IntPtr XEDIT {get; private set;}
		}
}


